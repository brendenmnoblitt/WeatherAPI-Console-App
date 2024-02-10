using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using WeatherConsoleApp;

namespace WeatherConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter your name:");
            string input = Console.ReadLine();
            Console.WriteLine($"Hello {input}!\n");

            // Call LocationAPI asynchronously
            Console.WriteLine("Enter a city:");
            string CityInput = Console.ReadLine();
            var location = ApiHandler.GetLocation(CityInput).GetAwaiter().GetResult();
            string locationKey = location.Key;
            Console.WriteLine($"Location: {location.LocalizedName}, {location.AdministrativeArea.ID}\n");

            
            // Call ForecastAPI asynchronously
            var forecast = ApiHandler.GetWeatherForecast(locationKey).GetAwaiter().GetResult();
            Console.WriteLine($"Report Date: {forecast.Headline.EffectiveDate}");
            Console.WriteLine($"Summary: {forecast.Headline.Text}\n");

            foreach (var dailyForecast in forecast.DailyForecasts)
            {
                Console.WriteLine($"============================================");
                Console.WriteLine($"Date: {dailyForecast.Date}");
                Console.WriteLine($"Temps: {dailyForecast.Temperature.Minimum.Value} --- {dailyForecast.Temperature.Maximum.Value}");
                Console.WriteLine($"============================================");

            }

            // Keep the console window open
            Console.ReadLine();
        }
    }

    internal static class ApiHandler
    {
        private const string ApiKey = ""; // Replace with your actual API key

        public static async Task<LocationModel> GetLocation(string query)
        {
            try
            {
                using var client = new HttpClient();
                string apiUrl = "http://dataservice.accuweather.com/locations/v1/cities/autocomplete";
                string queryString = $"?apikey={ApiKey}&q=" + query;
                var response = await client.GetAsync(apiUrl + queryString);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<LocationModel> locations = JsonSerializer.Deserialize<List<LocationModel>>(responseBody);
                    LocationModel topResult = locations[0];
                    return topResult;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public static async Task<WeatherForecastModel> GetWeatherForecast(string locationKey)
        {
            try
            {
                using var client = new HttpClient();
                string apiUrl = "http://dataservice.accuweather.com/forecasts/v1/daily/5day/";
                string queryString = $"{locationKey}?apikey={ApiKey}";
                var response = await client.GetAsync(apiUrl + queryString);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                    WeatherForecastModel forecast = JsonSerializer.Deserialize<WeatherForecastModel>(responseBody);
                    return forecast;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherConsoleApp
{
    public class WeatherForecastModel
    {
        public Headline Headline { get; set; }
        public List<DailyForecast> DailyForecasts { get; set; }
    }
    
    public class Headline
    {
        public DateTimeOffset EffectiveDate { get; set; }
        public long EffectiveEpochDate { get; set; }
        public int Severity { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public long EndEpochDate { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class DailyForecast
    {
        public DateTimeOffset Date { get; set; }
        public long EpochDate { get; set; }
        public Temperature Temperature { get; set; }
        public Forecast DayForecast { get; set; }
        public Forecast EveningForecast { get; set; }
    }

    public class Temperature
    {
        public TemperatureValue Minimum { get; set; }
        public TemperatureValue Maximum { get; set; }
    }

    public class TemperatureValue
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Forecast
    {
        public int Icon { get; set; }
        public string IconPhrase { get; set; }
        public bool HasPrecipitation { get; set; }
        public string? PrecipitationType { get; set; }
        public string? PrecipitationIntensity { get; set; }
    }

}

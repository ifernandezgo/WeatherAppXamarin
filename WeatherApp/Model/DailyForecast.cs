using System;
using Newtonsoft.Json;

namespace WeatherApp.Model
{
	public class DailyForecast
	{
        [JsonProperty("Date")]
        public String date { get; set; }

        [JsonProperty("Temperature")]
        public FiveDayTemperature temperature { get; set; }

        [JsonProperty("Day")]
        public DayNight day { get; set; }

        [JsonProperty("Nigth")]
        public DayNight night { get; set; }
    }
}


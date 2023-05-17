using System;
using Newtonsoft.Json;
namespace WeatherApp.Model
{
	public class FiveDayTemperature
	{
        [JsonProperty("Minimum")]
        public Metric minimum { get; set; }

        [JsonProperty("Maximum")]
        public Metric maximum { get; set; }
    }
}


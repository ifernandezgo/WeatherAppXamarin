using System;
using Newtonsoft.Json;

namespace WeatherApp.Model
{
	public class Metric
	{
        [JsonProperty("Value")]
        public Double value { get; set; }

        [JsonProperty("Unit")]
        public String unit { get; set; }
    }
}


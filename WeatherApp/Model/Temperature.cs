using System;
using Newtonsoft.Json;

namespace WeatherApp.Model
{
    public class Temperature
    {
        [JsonProperty("Metric")]
        public Metric metric { get; set; }
    }
}


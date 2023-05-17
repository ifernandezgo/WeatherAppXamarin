using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace WeatherApp.Model
{
	public class FiveDayForecast
	{
		[JsonProperty("DailyForecasts")]
		public List<DailyForecast> dailyForecasts { get; set; }
    }
}


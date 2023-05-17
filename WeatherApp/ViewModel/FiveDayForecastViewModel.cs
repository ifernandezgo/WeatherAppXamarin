using System;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel
{
	public class FiveDayForecastViewModel
	{
        public static Task<LocationKey> getLocationKey(String apiKey, String coordinates)
        {
            var apiResponse = RestService.For<APIService>("https://dataservice.accuweather.com/");
            return apiResponse.getLocationKey(apiKey, coordinates);
        }

        public static Task<FiveDayForecast> getFiveDayForecast(String locationKey, String apiKey)
        {
            var apiResponse = RestService.For<APIService>("https://dataservice.accuweather.com/");
            return apiResponse.getFiveDayForecast(locationKey, apiKey);
        }
    }
}


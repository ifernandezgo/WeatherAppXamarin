using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
namespace WeatherApp.Model
{
	public interface APIService
	{
		[Get("/locations/v1/cities/geoposition/search?apikey={apiKey}&q={coordinates}&language=es-ES&details=false")]
		Task<LocationKey> getLocationKey(String apiKey, String coordinates);

		[Get("/currentconditions/v1/{locationKey}?apikey={apiKey}&language=es-ES&details=false")]
		Task<List<WeatherForecast>> getCurrentConditions(String locationKey, String apiKey);

		[Get("/forecasts/v1/daily/5day/{locationKey}?apikey={apiKey}&language=es-ES&details=false&metric=true")]
		Task<FiveDayForecast> getFiveDayForecast(String locationKey, string apiKey);
	}
}


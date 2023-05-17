
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Location;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using WeatherApp.ViewModel;
using WeatherApp.View;


namespace WeatherApp.View
{
	[Activity (Label = "FiveDayForecastActivity")]			
	public class FiveDayForecastActivity : Activity
	{

        FusedLocationProviderClient fusedLocationProviderClient;
        RecyclerView rv_forecast;

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
            SetContentView(Resource.Layout.fragment_five_day_forecast);
            rv_forecast = FindViewById<RecyclerView>(Resource.Id.rv_forecast);
            fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(this);
            getCurrentConditions();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void getCurrentConditions()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Permission.Granted
                || ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) == Permission.Granted)
            {
                Task task = GetLastLocationFromDevice();
            }
            else
            {
                // The app does not have permission ACCESS_FINE_LOCATION
                Log.Debug("Location", "No access permission");
            }
        }

        async Task GetLastLocationFromDevice()
        {
            Android.Locations.Location location = await fusedLocationProviderClient.GetLastLocationAsync();

            if (location == null)
            {
                Log.Debug("Location", "No location");
            }
            else
            {
                var coordinates = location.Latitude + "," + location.Longitude;
                Log.Debug("Coordinates", coordinates);
                var apiKey = "m0CLj239gGRjerrGbedSe15OaTpq6OF3";
                try
                {
                    var locationKey = await FiveDayForecastViewModel.getLocationKey(apiKey, coordinates);
                    Log.Debug("LocationKey", locationKey.Key);
                    var currentConditions = await FiveDayForecastViewModel.getFiveDayForecast(locationKey.Key, apiKey);
                    var adapter = new ForecastAdapter(currentConditions.dailyForecasts);
                    rv_forecast.SetAdapter(adapter);
                }
                catch (Exception e)
                {
                    Log.Debug("Error", e.Message);
                }
            }
        }
    }
}


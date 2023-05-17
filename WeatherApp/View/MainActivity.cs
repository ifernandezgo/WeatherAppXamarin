using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Location;
using Android.Util;
using System.Threading.Tasks;
using WeatherApp.ViewModel;
using Refit;
using WeatherApp.Model;
using Android.Widget;
using Android.Content;
using WeatherApp.View;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        FusedLocationProviderClient fusedLocationProviderClient;
        Button btnPronostico;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            btnPronostico = FindViewById<Button>(Resource.Id.btnPronostico);
            btnPronostico.Click += showFiveDayForecast;
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
                    var locationKey = await MainViewModel.getLocationKey(apiKey, coordinates);
                    Log.Debug("LocationKey", locationKey.Key);
                    var currentConditions = await MainViewModel.getCurrentCoditions(locationKey.Key, apiKey);
                    Log.Debug("Current conditions", currentConditions[0].weatherText);
                    setUI(locationKey, currentConditions[0]);
                } catch(Exception e)
                {
                    Log.Debug("Error", e.Message);
                }
            }
        }

        public void setUI(LocationKey locationKey, WeatherForecast forecast)
        {
            TextView appTitle = FindViewById<TextView>(Resource.Id.appTitle);
            appTitle.Text = locationKey.LocalizedName;

            TextView dateText = FindViewById<TextView>(Resource.Id.dateText);
            dateText.Text = forecast.localObservationDateTime.Split("T")[0];

            TextView weatherText = FindViewById<TextView>(Resource.Id.weatherText);
            weatherText.Text = forecast.weatherText;

            TextView temperatureText = FindViewById<TextView>(Resource.Id.temperatureText);
            temperatureText.Text = forecast.temperature.metric.value.ToString() + " º" + forecast.temperature.metric.unit;

            ImageView weatherImage = FindViewById<ImageView>(Resource.Id.weatherImage);
            int resourceImg = Resources.GetIdentifier("image" + forecast.weatherIcon, "drawable", PackageName);
            weatherImage.SetImageResource(resourceImg);

            TextView precipitationText = FindViewById<TextView>(Resource.Id.precipitationText);
            if (forecast.hasPrecipitation)
            {
                if(forecast.precipitationType != null)
                {
                    precipitationText.Text = forecast.precipitationType;
                } else
                {
                    precipitationText.Text = "Precipitaciones";
                }
            }
            else
            {
                precipitationText.Text = "Sin precipitaciones";
            }
        }

        private void showFiveDayForecast(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(FiveDayForecastActivity));
            StartActivity(intent);
        }
    }
}


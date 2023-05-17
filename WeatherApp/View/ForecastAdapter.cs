using System;
using System.Collections.Generic;
using Android.Content.Res;
using Android.Views;
using Android.Widget;
//using AndroidX.RecyclerView.Widget;
using Android.Support.V7.Widget;
using WeatherApp.Model;

namespace WeatherApp.View
{
    public class ForecastAdapter: RecyclerView.Adapter
    {
        public List<DailyForecast> forecasts;
        public String packageName;
        public Resources resources;

        public ForecastAdapter(List<DailyForecast> forecastsList)
        {
            this.forecasts = forecastsList;
        }

        public override int ItemCount => forecasts.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ForecastViewHolder vh = holder as ForecastViewHolder;
            DailyForecast forecast = forecasts[position];
            vh.tvDate.Text = forecast.date.Split("T")[0];
            vh.tvMaximum.Text = "Maximum: " + forecast.temperature.maximum.value + " º" + forecast.temperature.maximum.unit;
            vh.tvMinimum.Text = "Minimum: " + forecast.temperature.minimum.value + " º" + forecast.temperature.minimum.unit;
            vh.tvDayText.Text = forecast.day.iconPhrase;
            int dayImageResource = resources.GetIdentifier("image" + forecast.day.Icon, "drawable", packageName);
            vh.dayImage.SetImageResource(dayImageResource);
            if(forecast.day.precipitationType != null)
            {
                vh.tvDayPrecipitation.Text = forecast.day.precipitationType;
            }
            else
            {
                vh.tvDayPrecipitation.Text = "No precipitation expected";
            }
            vh.tvNightText.Text = forecast.night.iconPhrase;
            int nigthImageResource = resources.GetIdentifier("image" + forecast.night.Icon, "drawable", packageName);
            vh.nightImage.SetImageResource(nigthImageResource);
            if (forecast.night.precipitationType != null)
            {
                vh.tvNightPrecipitation.Text = forecast.night.precipitationType;
            }
            else
            {
                vh.tvNightPrecipitation.Text = "No precipitation expected";
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            Android.Views.View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.layout_forecast, parent, false);
            this.packageName = parent.Context.PackageName;
            this.resources = parent.Context.Resources;
            ForecastViewHolder vh = new ForecastViewHolder(itemView);
            return vh;
        }
    }

    public class ForecastViewHolder : RecyclerView.ViewHolder
    {
        public TextView tvDate { get; set; }
        public TextView tvMaximum { get; set; }
        public TextView tvMinimum { get; set; }
        public TextView tvDayText { get; set; }
        public ImageView dayImage { get; set; }
        public TextView tvDayPrecipitation { get; set; }
        public TextView tvNightText { get; set; }
        public ImageView nightImage { get; set; }
        public TextView tvNightPrecipitation { get; set; }

        public ForecastViewHolder(Android.Views.View itemView) : base(itemView)
        {
            tvDate = itemView.FindViewById<TextView>(Resource.Id.tv_date);
            tvMaximum = itemView.FindViewById<TextView>(Resource.Id.tv_maximum);
            tvMinimum = itemView.FindViewById<TextView>(Resource.Id.tv_minimum);
            tvDayText = itemView.FindViewById<TextView>(Resource.Id.tv_day_text);
            dayImage = itemView.FindViewById<ImageView>(Resource.Id.day_image);
            tvDayPrecipitation = itemView.FindViewById<TextView>(Resource.Id.tv_day_precipitation);
            tvNightText = itemView.FindViewById<TextView>(Resource.Id.tv_night_text);
            nightImage = itemView.FindViewById<ImageView>(Resource.Id.night_image);
            tvNightPrecipitation = itemView.FindViewById<TextView>(Resource.Id.tv_night_precipitation);
        }
    }
}


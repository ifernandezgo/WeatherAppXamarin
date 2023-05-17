using System;
using Newtonsoft.Json;

namespace WeatherApp.Model
{
	public class DayNight
	{
        [JsonProperty("Icon")]
        public int Icon { get; set; }

        [JsonProperty("IconPhrase")]
        public String iconPhrase { get; set; }

        [JsonProperty("HasPrecipitation")]
        public Boolean hasPrecipitation { get; set; }

        [JsonProperty("PrecipitationType")]
        public String precipitationType { get; set; }

        [JsonProperty("PrecipitationIntensity")]
        public String precipitationIntensity { get; set; }
    }
}


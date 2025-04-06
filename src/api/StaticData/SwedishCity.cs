using System;
using System.Text.Json.Serialization;


namespace api.StaticData
{
    public class SwedishCity
    {
        [JsonPropertyName("county")]
        public string County { get; set; } = string.Empty;

        [JsonPropertyName("municipality")]
        public string Municipality { get; set; } = string.Empty;

        [JsonPropertyName("locality")]
        public string Locality { get; set; } = string.Empty;

        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }
    }
}

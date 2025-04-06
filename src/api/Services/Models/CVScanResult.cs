using System;
using System.Text.Json.Serialization;


namespace api.Services.Models
{
    public class CVScanResult
    {
        [JsonPropertyName("Skills")]
        public List<string> Skills { get; set; } = new List<string>();

        [JsonPropertyName("Location")]
        public string Location { get; set; } = string.Empty;
    }
}
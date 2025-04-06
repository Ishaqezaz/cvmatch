using System;
using api.Interfaces;
using System.Text.Json;
using api.Interfaces.Repository;
using api.Services.Models;
using api.Helpers;


namespace api.Services
{
    public class CVScannerService : ICVScannerService
    {
        private readonly IOpenAiRepository _openAiClient;
        private readonly IPdfReaderRepository _pdfReader;
        private readonly ICityRepository _cityRepository;
        private readonly IConfiguration _config;

        public CVScannerService(
            IConfiguration config,
            IOpenAiRepository openAiRepository,
            IPdfReaderRepository pdfReaderRepository,
            ICityRepository cityRepository)
        {
            _config = config;
            _openAiClient = openAiRepository;
            _pdfReader = pdfReaderRepository;
            _cityRepository = cityRepository;
        }

        public async Task<CVScanResult> ScanSkillsAndLocationAsync(Stream stream)
        {
            // read pdf
            string cvText = _pdfReader.ReadText(stream);

            string apiKey = _config["OpenAI:APIKey"] ?? throw new ArgumentNullException("Missing key");
            string model = _config["OpenAI:Model"] ?? throw new ArgumentNullException("Missing key");
            string prompt = _config["OpenAI:Prompt"] ?? throw new ArgumentNullException("Missing key");

            // gpt call to categorize
            string resultJson = await _openAiClient.SendMessageAsync(model, prompt, cvText, apiKey);
            return ParseOpenAiResponse(resultJson);
        }

        public List<string> CalculateNearbyCounties(string location, double radiusKm = 100)
        {
            if (string.IsNullOrWhiteSpace(location))
                return [];

            var cities = _cityRepository.GetCities();
            if (cities == null || cities.Count == 0)
                return [];

            var origin = cities.FirstOrDefault(c =>
                c.Locality.Equals(location, StringComparison.OrdinalIgnoreCase) ||
                c.Municipality.Equals(location, StringComparison.OrdinalIgnoreCase) ||
                c.County.Equals(location, StringComparison.OrdinalIgnoreCase));

            if (origin == null)
                return [];

            double lat1 = origin.Lat;
            double lon1 = origin.Lon;

            var nearbyCounties = cities
                .Where(c => GeoHelper.GetDistanceKm(lat1, lon1, c.Lat, c.Lon) <= radiusKm)
                .Select(c => c.County)
                .Distinct()
                .ToList(); // distance based on locality, filtered by county

            return nearbyCounties;
        }

        public CVScanResult ParseOpenAiResponse(string json)
        {
            try
            {
                using var doc = JsonDocument.Parse(json);
                var content = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                // clean up response
                var cleanContent = content!
                    .Replace("```json", "")
                    .Replace("```", "")
                    .Trim();

                // Handle cases where response might be just the array
                if (cleanContent.StartsWith("["))
                {
                    return new CVScanResult
                    {
                        Skills = JsonSerializer.Deserialize<List<string>>(cleanContent),
                        Location = string.Empty
                    };
                }

                return JsonSerializer.Deserialize<CVScanResult>(cleanContent)!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Failed to parse response: {json}", ex);
            }
        }
    }
}
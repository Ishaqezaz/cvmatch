using System;
using api.Interfaces.Repository;
using System.Text.Json;
using System.Text;


// external but i will keep it in here
namespace api.Repository
{
    public class OpenAiRepository : IOpenAiRepository
    {
        private readonly HttpClient _client;

        public OpenAiRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> SendMessageAsync(string model, string prompt, string cvText, string apiKey)
        {
            var requestBody = new
            {
                model,
                messages = new[]
                {
                new { role = "system", content = prompt },
                new { role = "user", content = cvText }
                }
            };

            string json = JsonSerializer.Serialize(requestBody);
            var response = await _client.PostAsync("https://api.openai.com/v1/chat/completions", new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                throw new Exception("OpenAI call failed: " + await response.Content.ReadAsStringAsync());

            string resultJson = await response.Content.ReadAsStringAsync();
            return resultJson;
        }
    }
}
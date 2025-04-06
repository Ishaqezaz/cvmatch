using System;


namespace api.Interfaces.Repository
{
    public interface IOpenAiRepository
    {
        Task<string> SendMessageAsync(string model, string prompt, string cvText, string apiKey);
    }
}
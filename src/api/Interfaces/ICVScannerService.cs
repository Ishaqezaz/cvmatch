using System;
using api.Services.Models;


namespace api.Interfaces
{
    public interface ICVScannerService
    {
        Task<CVScanResult> ScanSkillsAndLocationAsync(Stream stream);
        List<string> CalculateNearbyCounties(string location, double radiusKm = 100);
        CVScanResult ParseOpenAiResponse(string json);
    }
}
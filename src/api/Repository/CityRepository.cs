using System;
using api.StaticData;
using System.Text.Json;
using api.Interfaces;


public class CityRepository : ICityRepository
{
    public List<SwedishCity> GetCities()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "Resources", "svenska-orter.json");
        if (!File.Exists(filePath))
            throw new FileNotFoundException("svenska-orter.json is missing");

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<SwedishCity>>(json) ?? new List<SwedishCity>();
    }
}
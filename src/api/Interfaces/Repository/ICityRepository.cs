using System;
using api.StaticData;


namespace api.Interfaces
{
    public interface ICityRepository
    {
        List<SwedishCity> GetCities();
    }
}
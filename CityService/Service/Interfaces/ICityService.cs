using CityService.Domain;
using CityService.Dto;

public interface ICityService
{
    List<CityDTO> GetAllCities();
    CityDTO GetCityById(int id);
    CityDTO AddCity(City city);
    CityDTO GetRandomCity();
    List<CityDTO> GetCitiesByRegion(string region);
    void AddRegion(Region region);
}
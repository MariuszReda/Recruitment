using CityService.Domain;
using CityService.Dto;

namespace CityService.Service.Implementations
{
    public class CityService : ICityService
    {
        private List<City> cities;
        private List<Region> regions;

        public CityService()
        {
            cities = new List<City>();
            regions = new List<Region>();
        }

        public List<CityDTO> GetAllCities()
        {
            var cityDTOs = cities.Select(city => MapCityToDTO(city)).ToList();
            return cityDTOs;
        }

        public CityDTO GetCityById(int id)
        {
            var city = cities.FirstOrDefault(c => c.Id == id);
            return city != null ? MapCityToDTO(city) : null;
        }

        public CityDTO AddCity(City city)
        {
            city.Id = cities.Count + 1;
            cities.Add(city);
            return MapCityToDTO(city);
        }

        public CityDTO GetRandomCity()
        {
            var random = new Random();
            var randomCity = cities[random.Next(cities.Count)];
            return MapCityToDTO(randomCity);
        }

        public List<CityDTO> GetCitiesByRegion(string region)
        {
            var citiesInRegion = cities.Where(city => regions.Any(r => r.Country == city.Country && r.City == region)).ToList();
            return citiesInRegion.Select(city => MapCityToDTO(city)).ToList();
        }

        private CityDTO MapCityToDTO(City city)
        {
            var cityDTO = new CityDTO
            {
                Id = city.Id,
                Name = city.Name,
                Population = city.Population,
                Country = city.Country
            };

            // Automatyczne uzupełnienie regionu na podstawie kraju
            var region = regions.FirstOrDefault(r => r.Country == city.Country);
            if (region != null)
            {
                cityDTO.Region = region.City;
            }

            return cityDTO;
        }
        public void AddRegion(Region region)
        {
            regions.Add(region);
        }
    }
}

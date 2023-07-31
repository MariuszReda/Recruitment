using AutoMapper;
using CityService.Domain;
using CityService.Dto;

namespace CityService.Service.Implementations
{
    public class CityService : ICityService
    {
        private List<City> cities;
        private List<Region> regions;
        private IMapper _mapper;
        public CityService(IMapper mapper)
        {
            cities = new List<City>();
            regions = new List<Region>();
            this._mapper = mapper;
            ConfigureMapper();
        }

        private void ConfigureMapper()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<City, CityDTO>()
                   .ForMember(dest => dest.Region, opt =>
                   {
                       opt.MapFrom(src => GetCityRegion(src.Country));
                   });
            }).CreateMapper();
        }

        private string GetCityRegion(string country)
        {
            var region = regions.FirstOrDefault(r => r.Country == country);
            return region != null ? region.City : string.Empty;
        }

        public List<CityDTO> GetAllCities()
        {
            var cityDTOs = _mapper.Map<List<CityDTO>>(cities);
            return cityDTOs;
        }

        public CityDTO GetCityById(int id)
        {
            var city = cities.FirstOrDefault(c => c.Id == id);
            return city != null ? _mapper.Map<CityDTO>(city) : null;
        }

        public CityDTO AddCity(City city)
        {
            city.Id = cities.Count + 1;
            cities.Add(city);
            return _mapper.Map<CityDTO>(city);
        }

        public CityDTO AddCity1(City city, Region region)
        {
            city.Id = cities.Count + 1;
            cities.Add(city);

            var existingRegion = regions.FirstOrDefault(r => r.Country == region.Country);
            if (existingRegion == null)
            {
                regions.Add(region);
            }
            else
            {
                if (existingRegion.City != region.City)
                {
                    existingRegion.City = region.City;
                }
            }
            var cityDTO = _mapper.Map<CityDTO>(city);
            cityDTO.Region = region.City;

            return cityDTO;
        }

        public CityDTO GetRandomCity()
        {
            var random = new Random();
            var randomCity = cities[random.Next(cities.Count)];
            return _mapper.Map<CityDTO>(randomCity);
        }

        public List<CityDTO> GetCitiesByRegion(string region)
        {
            var citiesInRegion = cities.Where(city => regions.Any(r => r.Country == city.Country && r.City == region)).ToList();
            return _mapper.Map<List<CityDTO>>(citiesInRegion);
        }

        public void AddRegion(Region region)
        {
            regions.Add(region);
        }
    }
}

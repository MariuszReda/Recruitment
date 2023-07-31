using CityService.Domain;

namespace CityService
{
    public class RequestAddCity
    {
        public City City { get; set; }
        public Region Region { get; set; }
    }
}

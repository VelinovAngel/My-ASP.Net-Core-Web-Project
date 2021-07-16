namespace BikesBooking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.DTO;
    using Microsoft.EntityFrameworkCore;

    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<Country> countreisRepository;
        private readonly IDeletableEntityRepository<City> citirsRepository;

        public HomeService(
            IDeletableEntityRepository<Country> countreisRepository,
            IDeletableEntityRepository<City> citirsRepository)
        {
            this.countreisRepository = countreisRepository;
            this.citirsRepository = citirsRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs()
            => this.countreisRepository.All().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList()
            .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name.ToString()));

        public async Task<IEnumerable<CityDtoOutput>> GetAllCitiesByCountryIdAsync(int id)
            => await this.citirsRepository.All().AsQueryable()
            .Where(x => x.CountryId == id)
            .Select(x => new CityDtoOutput
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
    }
}

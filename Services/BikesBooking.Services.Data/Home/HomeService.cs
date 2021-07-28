namespace BikesBooking.Services.Data.Home
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
        private readonly IDeletableEntityRepository<Manufacturer> manufacturerRepository;

        public HomeService(
            IDeletableEntityRepository<Country> countreisRepository,
            IDeletableEntityRepository<City> citirsRepository,
            IDeletableEntityRepository<Manufacturer> manufacturerRepository)
        {
            this.countreisRepository = countreisRepository;
            this.citirsRepository = citirsRepository;
            this.manufacturerRepository = manufacturerRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetKeyValuePairsCoutries()
            => this.countreisRepository.All().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList()
            .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name.ToString()));

        public IEnumerable<KeyValuePair<string, string>> GetKeyValuePairsModels()
            => this.manufacturerRepository.All().Select(x => new
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

        public int GetAllCityCount()
            => this.citirsRepository.AllAsNoTracking().Count();
    }
}

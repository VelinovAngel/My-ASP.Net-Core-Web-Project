namespace BikesBooking.Services.Data.Dealer
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.DTO.Dealers;

    public class DealersService : IDealersService
    {
        private readonly IRepository<Dealer> dealerRepository;
        private readonly IRepository<Country> countryRepository;
        private readonly IRepository<City> cityRepository;

        public DealersService(
            IRepository<Dealer> dealer,
            IRepository<Country> countryRepository,
            IRepository<City> cityRepository)
        {
            this.dealerRepository = dealer;
            this.countryRepository = countryRepository;
            this.cityRepository = cityRepository;
        }

        public async Task CreateDealerAsync(CreateDealerDto dealer, string userId)
        {
            if (!this.countryRepository.AllAsNoTracking().Any(x => x.Name == dealer.Country))
            {
                await this.countryRepository.AddAsync(new Country { Name = dealer.Country });
                await this.countryRepository.SaveChangesAsync();
            }

            var countryId = this.countryRepository
                                .AllAsNoTracking()
                                .FirstOrDefault(x => x.Name == dealer.Country).Id;

            if (!this.cityRepository.AllAsNoTracking().Any(x => x.Name == dealer.City))
            {
                await this.cityRepository.AddAsync(new City
                {
                    Name = dealer.City,
                    CountryId = countryId,
                    Postcode = new Random().Next(1000, 99999),
                });

                await this.cityRepository.SaveChangesAsync();
            }

            var cityId = this.cityRepository
                                .AllAsNoTracking()
                                .FirstOrDefault(x => x.Name == dealer.City).Id;

            var currDealer = new Dealer
            {
                Name = dealer.Name,
                Address = dealer.Address,
                Description = dealer.Description,
                Email = dealer.Email,
                DealerId = userId,
                CityId = cityId,
            };

            await this.dealerRepository.AddAsync(currDealer);
            await this.dealerRepository.SaveChangesAsync();
        }

        public int GetDealerId(string userId)
            => this.dealerRepository.AllAsNoTracking()
            .Where(x => x.DealerId == userId)
            .Select(d => new { Id = d.Id })
            .FirstOrDefault().Id;

        public string GetCurrentDealerEmail(int id)
             => this.dealerRepository.AllAsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(x => x.Email)
                    .FirstOrDefault()
                    .ToString();

        public bool IsAlreadyDealerExist(string id)
            => this.dealerRepository.AllAsNoTracking().Any(x => x.DealerId == id);
    }
}

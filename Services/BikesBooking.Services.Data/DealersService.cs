namespace BikesBooking.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.DTO.Dealers;

    public class DealersService : IDealersService
    {
        private readonly IRepository<PublicDealer> publicDelaer;
        private readonly IRepository<Country> countryRepository;
        private readonly IRepository<City> cityRepository;
        private readonly IRepository<Provider> providerRepository;

        public DealersService(
            IRepository<PublicDealer> publicDelaer,
            IRepository<Country> countryRepository,
            IRepository<City> cityRepository,
            IRepository<Provider> providerRepository)
        {
            this.publicDelaer = publicDelaer;
            this.countryRepository = countryRepository;
            this.cityRepository = cityRepository;
            this.providerRepository = providerRepository;
        }

        public async Task CreatePublicDealer(CreateDealerDto dealer, string userId)
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

            var provider = new Provider
            {
                CountryId = countryId,
            };
            await this.providerRepository.AddAsync(provider);
            await this.providerRepository.SaveChangesAsync();

            var publicDealer = new PublicDealer
            {
                Name = dealer.Name,
                Address = dealer.Address,
                Description = dealer.Description,
                Email = dealer.Email,
                ProviderId = provider.Id,
                PublicDealerId = userId,
            };

            await this.publicDelaer.AddAsync(publicDealer);
            await this.publicDelaer.SaveChangesAsync();
        }

        public bool IsAlreadyPublicDealerExist(string id)
            => this.publicDelaer.AllAsNoTracking().Any(x => x.PublicDealerId == id);
    }
}

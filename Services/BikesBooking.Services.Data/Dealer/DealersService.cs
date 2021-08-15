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

        public async Task CreateDealerAsync(CreateDealerDto dealer, string userId, string imgUrl)
        {
            await this.ValidaiteDealerData(dealer);

            var cityId = this.cityRepository
                                .AllAsNoTracking()
                                .FirstOrDefault(x => x.Name == dealer.City).Id;

            var currDealer = new Dealer
            {
                Name = dealer.Name,
                Address = dealer.Address,
                Description = dealer.Description,
                Email = dealer.Email,
                UserId = userId,
                CityId = cityId,
                ImageFile = imgUrl,
            };

            await this.dealerRepository.AddAsync(currDealer);
            await this.dealerRepository.SaveChangesAsync();
        }

        public int GetDealerId(string userId)
            => this.dealerRepository.All()
            .Where(x => x.UserId == userId)
            .FirstOrDefault().Id;

        public string GetCurrentDealerEmail(int id)
             => this.dealerRepository.AllAsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(x => x.Email)
                    .FirstOrDefault()
                    .ToString();

        public bool IsDealer(string userId)
            => this.dealerRepository.AllAsNoTracking().Any(x => x.UserId == userId);

        public string GetDealerIdByUser(string userId)
            => this.dealerRepository.All().FirstOrDefault(x => x.UserId == userId).UserId;

        public CreateDealerDto GetCurrentDealerInfo(int id)
            => this.dealerRepository.All()
            .Select(x => new CreateDealerDto
            {
                Name = x.Name,
                Address = x.Address,
                Country = x.City.Country.Name,
                City = x.City.Name,
                Description = x.Description,
                Email = x.Email,
            }).FirstOrDefault();

        public async Task<bool> Edit(CreateDealerDto dealer, int id, string imgUrl)
        {
            var currDealer = this.dealerRepository.All().FirstOrDefault(x => x.Id == id);

            if (currDealer == null)
            {
                return false;
            }

            var isDealerDataValid = await this.ValidaiteDealerData(dealer);

            if (!isDealerDataValid)
            {
                return false;
            }

            var city = this.cityRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == dealer.City).Id;

            currDealer.Name = dealer.Name;
            currDealer.Address = dealer.Address;
            currDealer.City.Id = city;
            currDealer.Description = dealer.Description;
            currDealer.Email = dealer.Email;
            currDealer.ImageFile = imgUrl;

            this.dealerRepository.Update(currDealer);
            await this.dealerRepository.SaveChangesAsync();

            return true;
        }

        private async Task<bool> ValidaiteDealerData(CreateDealerDto dealer)
        {
            try
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

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

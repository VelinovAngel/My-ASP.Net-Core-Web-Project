namespace BikesBooking.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using BikesBooking.Web.ViewModels.Motor;

    public class MotorcycleService : IMotorcycleService
    {
        private readonly IRepository<Model> modelsRepository;
        private readonly IRepository<Manufacturer> manufacturerRepository;
        private readonly IRepository<Motorcycle> motorcycleRepository;
        private readonly IRepository<Color> colorRepository;
        private readonly IRepository<Country> countryRepository;
        private readonly IRepository<City> cityRepository;

        public MotorcycleService(
            IRepository<Model> modelsRepository,
            IRepository<Manufacturer> manufacturerRepository,
            IRepository<Motorcycle> motorcycleRepository,
            IRepository<Color> colorRepository,
            IRepository<Country> countryRepository,
            IRepository<City> cityRepository)
        {
            this.modelsRepository = modelsRepository;
            this.manufacturerRepository = manufacturerRepository;
            this.motorcycleRepository = motorcycleRepository;
            this.colorRepository = colorRepository;
            this.countryRepository = countryRepository;
            this.cityRepository = cityRepository;
        }

        public async Task CreateMotorcycle(AddMotorcycleModel createMotorcycle)
        {
            if (!this.modelsRepository.AllAsNoTracking().Any(x => x.Name == createMotorcycle.Model))
            {
                await this.modelsRepository.AddAsync(new Model { Name = createMotorcycle.Model });
                await this.modelsRepository.SaveChangesAsync();
            }

            if (!this.manufacturerRepository.AllAsNoTracking().Any(x => x.Name == createMotorcycle.Manufacturer))
            {
                await this.manufacturerRepository.AddAsync(new Manufacturer { Name = createMotorcycle.Manufacturer });
                await this.manufacturerRepository.SaveChangesAsync();
            }

            if (!this.colorRepository.AllAsNoTracking().Any(x => x.Name == createMotorcycle.Color))
            {
                await this.colorRepository.AddAsync(new Color { Name = createMotorcycle.Color });
                await this.colorRepository.SaveChangesAsync();
            }

            if (!this.countryRepository.AllAsNoTracking().Any(x => x.Name == createMotorcycle.Country))
            {
                await this.countryRepository.AddAsync(new Country { Name = createMotorcycle.Country });
                await this.countryRepository.SaveChangesAsync();
            }

            var countryId = this.countryRepository
                                .AllAsNoTracking()
                                .FirstOrDefault(x => x.Name == createMotorcycle.Country).Id;

            if (!this.cityRepository.AllAsNoTracking().Any(x => x.Name == createMotorcycle.City))
            {
                await this.cityRepository.AddAsync(new City
                {
                    Name = createMotorcycle.City,
                    CountryId = countryId,
                    Postcode = new Random().Next(1000, 99999),
                });
                await this.cityRepository.SaveChangesAsync();
            }


            var model = this.modelsRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.Model);
            var manufacturer = this.manufacturerRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.Manufacturer);
            var color = this.colorRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.Color);
            var city = this.cityRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.City);

            var motorcycle = new Motorcycle
            {
                ManufacturerId = manufacturer.Id,
                ModelId = model.Id,
                ColorId = color.Id,
                CityId = city.Id,
                CubicCentimetre = createMotorcycle.CubicCentimetre,
                Url = createMotorcycle.Url,
                Available = createMotorcycle.Available,
                Description = createMotorcycle.Description,
                Price = createMotorcycle.Price,
            };

            await this.motorcycleRepository.AddAsync(motorcycle);
            await this.motorcycleRepository.SaveChangesAsync();
        }
    }
}

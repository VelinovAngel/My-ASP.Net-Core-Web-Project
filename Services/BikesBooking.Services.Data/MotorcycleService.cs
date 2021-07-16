namespace BikesBooking.Services.Data
{
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
            var model = this.modelsRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.Model);

            var manufacturer = this.manufacturerRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.Manufacturer);

            var color = this.colorRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.Color);

            var country = this.countryRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.Country);

            var city = this.colorRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.City);

            if (model == null)
            {
                await this.modelsRepository.AddAsync(new Model { Name = createMotorcycle.Color });
            }

            if (manufacturer == null)
            {
                await this.manufacturerRepository.AddAsync(new Manufacturer { Name = createMotorcycle.Color });
            }

            if (color == null)
            {
                await this.colorRepository.AddAsync(new Color { Name = createMotorcycle.Color });
            }

            if (country == null)
            {
                await this.countryRepository.AddAsync(new Country { Name = createMotorcycle.Country });
            }

            var countryId = this.countryRepository
                                .AllAsNoTracking()
                                .FirstOrDefault(x => x.Name == createMotorcycle.Country).Id;

            if (city == null)
            {
                await this.cityRepository.AddAsync(new City { Name = createMotorcycle.City, CountryId = countryId });
            }

            var fisrtModel = this.modelsRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.Model);
            var firstManufacturer = this.manufacturerRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.Manufacturer);
            var firstColor = this.colorRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.Color);
            var firstCity = this.cityRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == createMotorcycle.City);

            var motorcycle = new Motorcycle
            {
                ManufacturerId = firstManufacturer.Id,
                Model = model,
                ColorId = firstColor.Id,
                CityId = firstCity.Id,
                CubicCentimetre = createMotorcycle.CubicCentimetre,
                Url = createMotorcycle.Url,
                Available = createMotorcycle.Available,
                Review = new Review { Description = createMotorcycle.Review },
                Price = createMotorcycle.Price,
            };

            await this.motorcycleRepository.AddAsync(motorcycle);
            await this.motorcycleRepository.SaveChangesAsync();
        }
    }
}

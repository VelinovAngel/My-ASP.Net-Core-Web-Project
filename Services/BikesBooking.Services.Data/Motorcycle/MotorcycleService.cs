namespace BikesBooking.Services.Data.Motorcycle
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.Dealer;
    using BikesBooking.Services.Data.DTO.Clients;
    using BikesBooking.Services.Data.DTO.MotorcycleModels;
    using BikesBooking.Services.Data.Votes;
    using Microsoft.EntityFrameworkCore;

    public class MotorcycleService : IMotorcycleService
    {
        private readonly IRepository<Model> modelsRepository;
        private readonly IRepository<Manufacturer> manufacturerRepository;
        private readonly IRepository<Motorcycle> motorcycleRepository;
        private readonly IRepository<Color> colorRepository;
        private readonly IRepository<Country> countryRepository;
        private readonly IRepository<City> cityRepository;
        private readonly IRepository<Offer> offerRepository;
        private readonly IVoteService votesService;

        public MotorcycleService(
            IRepository<Model> modelsRepository,
            IRepository<Manufacturer> manufacturerRepository,
            IRepository<Motorcycle> motorcycleRepository,
            IRepository<Color> colorRepository,
            IRepository<Country> countryRepository,
            IRepository<City> cityRepository,
            IRepository<Offer> offerRepository,
            IVoteService votesService)
        {
            this.modelsRepository = modelsRepository;
            this.manufacturerRepository = manufacturerRepository;
            this.motorcycleRepository = motorcycleRepository;
            this.colorRepository = colorRepository;
            this.countryRepository = countryRepository;
            this.cityRepository = cityRepository;
            this.offerRepository = offerRepository;
            this.votesService = votesService;
        }

        public async Task<int> CreateMotorcycleAsync(MotorcycleServiceDto createMotorcycle, int dealerId)
        {
            await this.ValidationMotorcycle(createMotorcycle);

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

            var typeVelue = (int)createMotorcycle.Type;
            var type = (BikesBooking.Data.Models.Enum.Type)typeVelue;

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
                TypeMotor = type,
                DealerId = dealerId,
            };

            await this.motorcycleRepository.AddAsync(motorcycle);
            await this.motorcycleRepository.SaveChangesAsync();

            return motorcycle.Id;
        }

        public async Task<bool> Edit(MotorcycleServiceDto motorcycle, int id)
        {
            var motorcycleData = this.motorcycleRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);

            if (motorcycleData == null)
            {
                return false;
            }

            await this.ValidationMotorcycle(motorcycle);

            var model = this.modelsRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == motorcycle.Model);
            var manufacturer = this.manufacturerRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == motorcycle.Manufacturer);
            var color = this.colorRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == motorcycle.Color);
            var city = this.cityRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Name == motorcycle.City);

            var typeVelue = (int)motorcycle.Type;
            var type = (BikesBooking.Data.Models.Enum.Type)typeVelue;

            motorcycleData.ManufacturerId = manufacturer.Id;
            motorcycleData.ModelId = model.Id;
            motorcycleData.ColorId = color.Id;
            motorcycleData.CityId = city.Id;
            motorcycleData.CubicCentimetre = motorcycle.CubicCentimetre;
            motorcycleData.Url = motorcycle.Url;
            motorcycleData.Available = motorcycle.Available;
            motorcycleData.Description = motorcycle.Description;
            motorcycleData.Price = motorcycle.Price;
            motorcycleData.TypeMotor = type;

            this.motorcycleRepository.Update(motorcycleData);
            await this.motorcycleRepository.SaveChangesAsync();

            return true;
        }

        public async Task<MotorcycleQueryServiceModel> GetCollectionOfMotorsAsync(int currentPage, int motorcyclesPerPage, int dealerId)
        {
            var motorcycles = await this.motorcycleRepository.AllAsNoTracking()
              .AsQueryable()
              .Where(x => x.DealerId == dealerId)
              .OrderByDescending(x => x.CreatedOn)
              .Skip((currentPage - 1) * motorcyclesPerPage)
              .Take(motorcyclesPerPage)
              .Select(x => new MotorcycleDetailsModel
              {
                  Id = x.Id,
                  Manufacturer = x.Manufacturer.Name,
                  Model = x.Model.Name,
                  Color = x.Color.Name,
                  CubicCentimetre = x.CubicCentimetre,
                  Country = x.City.Country.Name,
                  City = x.City.Name,
                  Price = x.Price,
                  Available = x.Available,
                  Url = x.Url,
                  Type = (TypeOfMotors)x.TypeMotor,
                  Description = x.Description,
                  AddedOn = x.ModifiedOn.HasValue ? x.ModifiedOn.Value : x.CreatedOn,
              })
              .ToListAsync();

            var totalCounts = this.modelsRepository.AllAsNoTracking().Count();

            return new MotorcycleQueryServiceModel
            {
                TotalMotorcycles = totalCounts,
                CurrentPage = currentPage,
                MotorcyclesPerPage = motorcyclesPerPage,
                Motorcycle = motorcycles,
            };
        }

        public async Task<OfferSigleMotorcycleDto> GetMotorcycleByIdAsync(int id)
            => await this.motorcycleRepository.AllAsNoTracking()
            .AsQueryable()
            .Where(x => x.Id == id)
            .Select(x => new OfferSigleMotorcycleDto
            {
                ModelId = x.Id,
                Manufacturer = x.Manufacturer.Name,
                Model = x.Model.Name,
                CubicCentimetre = x.CubicCentimetre,
                Color = x.Color.Name,
                Price = x.Price,
                Url = x.Url,
                Available = x.Available,
                Type = x.TypeMotor.ToString(),
                DealerId = x.Dealer.UserId,
                BeginBooking = x.Offer.PickUpDate,
                FinalBooking = x.Offer.DropOffDate,
            })
            .FirstOrDefaultAsync();

        public async Task RemoveMotorcycleAsync(int id)
        {
            var motorcycle = await this.motorcycleRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (motorcycle == null)
            {
                return;
            }

            this.motorcycleRepository.Delete(motorcycle);
            await this.motorcycleRepository.SaveChangesAsync();
        }

        public MotorcycleDetailViewModel Details(int id)
            => this.motorcycleRepository.AllAsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new MotorcycleDetailViewModel
            {
                Id = x.Id,
                Manufacturer = x.Manufacturer.Name,
                Model = x.Model.Name,
                Color = x.Color.Name,
                CubicCentimetre = x.CubicCentimetre,
                Country = x.City.Country.Name,
                City = x.City.Name,
                Price = x.Price,
                Year = x.Manufacturer.Year,
                Available = x.Available,
                Url = x.Url,
                Type = (TypeOfMotors)x.TypeMotor,
                Description = x.Description,
                AddedOn = x.ModifiedOn.HasValue ? x.ModifiedOn.Value : x.CreatedOn,
                DealerId = x.Dealer.UserId,
                Dealer = x.Dealer.Name,
                PickUpDate = x.Offer.PickUpDate,
                DropOffDate = x.Offer.DropOffDate,
            })
            .FirstOrDefault();

        public int GetMotorcycleCount()
            => this.motorcycleRepository.AllAsNoTracking().Count();

        public int GetNotAvailableMotorcycleCount()
            => this.motorcycleRepository.AllAsNoTracking()
            .Where(x => x.Available == false)
            .Count();

        public async Task ChangeStateOfMotorcycle(bool available, int id)
        {
            var currentMotor = this.motorcycleRepository
               .AllAsNoTracking()
               .Where(x => x.Id == id)
               .FirstOrDefault();

            currentMotor.Available = available;

            this.motorcycleRepository.Update(currentMotor);
            await this.motorcycleRepository.SaveChangesAsync();
        }

        public async Task OfferCurrentMotor(OfferPeriodForMotorDto offerPeriodForMotorDto, int id)
        {
            var currentMotor = this.motorcycleRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if (!this.offerRepository
                               .AllAsNoTracking()
                               .Where(x => x.PickUpDate == offerPeriodForMotorDto.PickUpDate &&
                                           x.DropOffDate == offerPeriodForMotorDto.DropOffDate)
                               .Any())
            {
                var offer = await this.AddOffer(offerPeriodForMotorDto);
            }

            var offerId = this.offerRepository
                               .AllAsNoTracking()
                               .Where(x => x.PickUpDate == offerPeriodForMotorDto.PickUpDate &&
                                           x.DropOffDate == offerPeriodForMotorDto.DropOffDate)
                               .FirstOrDefault().Id;

            currentMotor.OfferId = offerId;
            currentMotor.Available = true;

            this.motorcycleRepository.Update(currentMotor);
            await this.motorcycleRepository.SaveChangesAsync();
        }

        public bool IsByDealer(int motorId, int dealerId)
            => this.motorcycleRepository.AllAsNoTracking()
                                        .Any(x => x.Id == motorId && x.Dealer.Id == dealerId);

        public async Task<MotorcycleQueryServiceModel> GetFreeMotors(int currentPage, int motorcyclesPerPage, DateTime pickUpDate, DateTime dropOffDate)
        {
            var motorcycles = await this.motorcycleRepository.AllAsNoTracking()
             .AsQueryable()
             .Where(x => x.Available == true && x.Offer.PickUpDate <= pickUpDate && x.Offer.DropOffDate >= dropOffDate)
             .OrderByDescending(x => x.CreatedOn)
             .Skip((currentPage - 1) * motorcyclesPerPage)
             .Take(motorcyclesPerPage)
             .Select(x => new MotorcycleDetailsModel
             {
                 Id = x.Id,
                 Manufacturer = x.Manufacturer.Name,
                 Model = x.Model.Name,
                 Color = x.Color.Name,
                 CubicCentimetre = x.CubicCentimetre,
                 Country = x.City.Country.Name,
                 City = x.City.Name,
                 AverageVote = this.votesService.GetAverageVote(x.Id),
                 Year = x.Manufacturer.Year,
                 Price = x.Price,
                 Available = x.Available,
                 Url = x.Url,
                 Type = (TypeOfMotors)x.TypeMotor,
                 Description = x.Description,
                 AddedOn = x.ModifiedOn.HasValue ? x.ModifiedOn.Value : x.CreatedOn,
             })
             .ToListAsync();

            var totalCounts = this.modelsRepository.AllAsNoTracking().Count();

            return new MotorcycleQueryServiceModel
            {
                TotalMotorcycles = totalCounts,
                CurrentPage = currentPage,
                MotorcyclesPerPage = motorcyclesPerPage,
                Motorcycle = motorcycles,
            };
        }

        private async Task<Offer> AddOffer(OfferPeriodForMotorDto offerPeriodForMotorDto)
        {
            var offer = new Offer
            {
                PickUpDate = offerPeriodForMotorDto.PickUpDate,
                DropOffDate = offerPeriodForMotorDto.DropOffDate,
                StatisticsBooked = 0,
            };
            await this.offerRepository.AddAsync(offer);
            await this.offerRepository.SaveChangesAsync();
            return offer;
        }

        private async Task ValidationMotorcycle(MotorcycleServiceDto createMotorcycle)
        {
            if (!this.modelsRepository.AllAsNoTracking().Any(x => x.Name == createMotorcycle.Model))
            {
                await this.modelsRepository.AddAsync(new Model { Name = createMotorcycle.Model });
                await this.modelsRepository.SaveChangesAsync();
            }

            if (!this.manufacturerRepository.AllAsNoTracking().Any(x => x.Name == createMotorcycle.Manufacturer))
            {
                await this.manufacturerRepository.AddAsync(new Manufacturer { Name = createMotorcycle.Manufacturer, Year = createMotorcycle.Year });
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
        }
    }
}

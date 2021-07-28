namespace BikesBooking.Services.Data.Motorcycle
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.Motorcycle;
    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public interface IMotorcycleService
    {
        Task CreateMotorcycleAsync(AddMotorcycleDto createMotorcycle, int dealerId);

        Task<MotorcycleQueryServiceModel> GetCollectionOfMotorsAsync(
            int currentPage,
            int motorcyclesPerPage,
            int dealerId);

        Task<OfferSigleMotorcycleDto> GetMotorcycleByIdAsync(int id);

        Task OfferCurrentMotor(OfferPeriodForMotorDto offerPeriodForMotorDto, int id);

        Task RemoveMotorcycleAsync(int id);

        public int GetMotorcycleCount();

        public int GetNotAvailableMotorcycleCount();
    }
}

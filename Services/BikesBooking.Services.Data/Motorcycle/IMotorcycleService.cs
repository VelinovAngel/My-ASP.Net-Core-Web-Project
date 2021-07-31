namespace BikesBooking.Services.Data.Motorcycle
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public interface IMotorcycleService
    {
        Task<int> CreateMotorcycleAsync(MotorcycleServiceDto createMotorcycle, int dealerId);

        Task<bool> Edit(MotorcycleServiceDto motorcycle, int id);

        Task<MotorcycleQueryServiceModel> GetCollectionOfMotorsAsync(
            int currentPage,
            int motorcyclesPerPage,
            int dealerId);

        Task<OfferSigleMotorcycleDto> GetMotorcycleByIdAsync(int id);

        Task OfferCurrentMotor(OfferPeriodForMotorDto offerPeriodForMotorDto, int id);

        Task RemoveMotorcycleAsync(int id);

        MotorcycleDetailsModel Details(int id);

        bool IsByDealer(int motorId, int dealerId);

        public int GetMotorcycleCount();

        public int GetNotAvailableMotorcycleCount();
    }
}

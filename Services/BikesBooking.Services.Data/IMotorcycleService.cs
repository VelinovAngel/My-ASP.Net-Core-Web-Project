namespace BikesBooking.Services.Data
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.Motorcycle;
    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public interface IMotorcycleService
    {
        Task CreateMotorcycleAsync(AddMotorcycleDto createMotorcycle);

        Task<MotorcycleQueryServiceModel> GetCollectionOfMotorsAsync(int currentPage, int motorcyclesPerPage);

        Task<OfferSigleMotorcycleDto> GetMotorcycleByIdAsync(int id);

        Task RemoveMotorcycleAsync(int id);
    }
}

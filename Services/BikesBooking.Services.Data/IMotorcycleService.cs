namespace BikesBooking.Services.Data
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.Motorcycle;
    using BikesBooking.Web.ViewModels.Motor;

    public interface IMotorcycleService
    {
        Task CreateMotorcycleAsync(AddMotorcycleModel createMotorcycle);

        Task<MotorcycleQueryServiceModel> GetCollectionOfMotorsAsync(int currentPage, int motorcyclesPerPage);

        Task<OfferSigleMotorcycleModel> GetMotorcycleByIdAsync(int id);
    }
}

namespace BikesBooking.Services.Data
{
    using System.Threading.Tasks;

    using BikesBooking.Web.ViewModels.Motor;

    public interface IMotorcycleService
    {
        Task CreateMotorcycle(AddMotorcycleModel createMotorcycle);
    }
}

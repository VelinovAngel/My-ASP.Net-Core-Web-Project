namespace BikesBooking.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO;
    using BikesBooking.Web.ViewModels.Motor;

    public interface IMotorcycleService
    {
        Task CreateMotorcycleAsync(AddMotorcycleModel createMotorcycle);

        Task<IEnumerable<MotorcycleDtoOutput>> GetCollectionOfMotorsAsync();
    }
}

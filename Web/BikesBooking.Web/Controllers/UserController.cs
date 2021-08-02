namespace BikesBooking.Web.Controllers
{
    using BikesBooking.Services.Data.DTO.MotorcycleModels;
    using BikesBooking.Services.Data.Motorcycle;
    using BikesBooking.Web.ViewModels.Motor;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class UserController : Controller
    {
        private readonly IMotorcycleService motorcycleService;

        public UserController(IMotorcycleService motorcycleService)
        {
            this.motorcycleService = motorcycleService;
        }

        public async Task<IActionResult> FreeMotors([FromQuery] AllMotorcylesQueryDto query, SearchMotorModel input)
        {
            var motorcycleResult = await this.motorcycleService.GetFreeMotors(query.CurrentPage, AllMotorcyclesQueryModel.MotorcyclesPerPage, input.PickUpDate, input.DropOffDate);
            query.TotalMotorcycle = motorcycleResult.TotalMotorcycles;
            query.Motors = motorcycleResult.Motorcycle;
            return this.View(query);
        }
    }
}

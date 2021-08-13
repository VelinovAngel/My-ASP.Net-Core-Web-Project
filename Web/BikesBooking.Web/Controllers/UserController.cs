namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.MotorcycleModels;
    using BikesBooking.Services.Data.Motorcycle;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        private readonly IMotorcycleService motorcycleService;

        public UserController(IMotorcycleService motorcycleService)
        {
            this.motorcycleService = motorcycleService;
        }

        public async Task<IActionResult> FreeMotors([FromQuery] AllFreeMotorcylesQueryDto query, SearchMotorcycleInputModel inputModel)
        {
            var motorcycleResult = await this.motorcycleService.GetFreeMotors(query.CurrentPage, AllFreeMotorcylesQueryDto.MotorcyclesPerPage, inputModel);
            query.TotalMotorcycle = motorcycleResult.TotalMotorcycles;
            query.Motors = motorcycleResult.Motorcycle;
            return this.View(query);
        }
    }
}

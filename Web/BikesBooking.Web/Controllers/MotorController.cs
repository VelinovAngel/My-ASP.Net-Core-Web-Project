namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data;
    using BikesBooking.Services.Data.DTO.MotorcycleModels;
    using BikesBooking.Web.Infrastructure;
    using BikesBooking.Web.ViewModels.Motor;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MotorController : Controller
    {
        private readonly IMotorcycleService motorcycleService;

        public MotorController(IMotorcycleService motorcycleService)
        {
            this.motorcycleService = motorcycleService;
        }

        public IActionResult Add()
        {
            var currentUserId = this.User.GetId();
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMotorcycleDto motorcycle)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.motorcycleService.CreateMotorcycleAsync(motorcycle);

            this.TempData["Message"] = "Motorcycles added successful!";

            return this.RedirectToAction("All", "Motor");
        }

        public async Task<IActionResult> All([FromQuery] AllMotorcylesQueryDto query)
        {
            var motorcycleResult = await this.motorcycleService.GetCollectionOfMotorsAsync(query.CurrentPage, AllMotorcyclesQueryModel.MotorcyclesPerPage);
            query.TotalMotorcycle = motorcycleResult.TotalMotorcycles;
            query.Motors = motorcycleResult.Motorcycle;
            return this.View(query);
        }

        public async Task<IActionResult> Offer(int id)
        {
            var model = await this.motorcycleService.GetMotorcycleByIdAsync(id);
            return this.View(model);
        }

        public IActionResult Edit(int id)
        {
            return this.View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            await this.motorcycleService.RemoveMotorcycleAsync(id);

            return this.RedirectToAction("All", "Motor");
        }
    }
}

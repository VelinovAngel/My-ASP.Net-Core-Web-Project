namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data;
    using BikesBooking.Web.ViewModels.Motor;
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
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMotorcycleModel motorcycle)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.motorcycleService.CreateMotorcycleAsync(motorcycle);

            this.TempData["Message"] = "Motorcycles added successful!";

            return this.Redirect("/Motor/All");
        }

        public async Task<IActionResult> All()
        {
            var motorcycle = await this.motorcycleService.GetCollectionOfMotorsAsync();
            return this.View(motorcycle);
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
    }
}

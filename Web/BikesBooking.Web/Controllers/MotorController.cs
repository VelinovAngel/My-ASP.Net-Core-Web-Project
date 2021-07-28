namespace BikesBooking.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.Dealer;
    using BikesBooking.Services.Data.DTO.MotorcycleModels;
    using BikesBooking.Services.Data.Motorcycle;
    using BikesBooking.Web.Infrastructure;
    using BikesBooking.Web.ViewModels.Motor;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MotorController : Controller
    {
        private readonly IMotorcycleService motorcycleService;
        private readonly IDealersService dealersService;

        public MotorController(
            IMotorcycleService motorcycleService,
            IDealersService dealersService)
        {
            this.motorcycleService = motorcycleService;
            this.dealersService = dealersService;
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMotorcycleDto motorcycle)
        {
            int userId = this.GetUserId();

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.motorcycleService.CreateMotorcycleAsync(motorcycle, userId);

            this.TempData["Message"] = "Motorcycles added successful!";

            return this.RedirectToAction("All", "Motor");
        }

        [Authorize]
        public async Task<IActionResult> All([FromQuery] AllMotorcylesQueryDto query)
        {
            var userId = this.GetUserId();
            var motorcycleResult = await this.motorcycleService.GetCollectionOfMotorsAsync(query.CurrentPage, AllMotorcyclesQueryModel.MotorcyclesPerPage, userId);
            query.TotalMotorcycle = motorcycleResult.TotalMotorcycles;
            query.Motors = motorcycleResult.Motorcycle;
            return this.View(query);
        }

        [Authorize]
        public async Task<IActionResult> Offer(int id)
        {
            var model = await this.motorcycleService.GetMotorcycleByIdAsync(id);
            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> OfferThisModel(int id)
        {
            var motor = new OfferThisModelForm
            {
                Motor = await this.motorcycleService.GetMotorcycleByIdAsync(id),
                PickUpDate = DateTime.UtcNow,
                DropOffDate = DateTime.UtcNow.AddDays(1),
            };

            return this.View(motor);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> OfferThisModel(OfferThisModelForm offerThisModelForm, [FromRoute]int id)
        {
            var pickUpDate = offerThisModelForm.PickUpDate;
            var dropOffDate = offerThisModelForm.DropOffDate;
            var offer = new OfferPeriodForMotorDto
            {
                PickUpDate = pickUpDate,
                DropOffDate = dropOffDate,
            };
            await this.motorcycleService.OfferCurrentMotor(offer, id);
            return this.RedirectToAction("All", "Motor");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {
            await this.motorcycleService.RemoveMotorcycleAsync(id);

            return this.RedirectToAction("All", "Motor");
        }

        private int GetUserId()
        {
            var currentUserId = this.User.GetId();
            var userId = this.dealersService.GetDealerId(currentUserId);
            return userId;
        }
    }
}

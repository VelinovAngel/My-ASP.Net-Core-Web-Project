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

    [Authorize(Roles = "Dealer, Administrator")]
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

        public IActionResult Add()
        {
            var userId = this.User.GetId();

            if (!this.dealersService.IsDealer(userId))
            {
                return this.RedirectToAction("Create", "Dealer");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(MotorcycleServiceDto motorcycle)
        {
            var dealerId = this.GetUserId();

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.motorcycleService.CreateMotorcycleAsync(motorcycle, dealerId);

            this.TempData["Message"] = "Motorcycles added successful!";

            return this.RedirectToAction("All", "Motor");
        }

        [Authorize]
        public async Task<IActionResult> All([FromQuery] AllMotorcylesQueryDto query)
        {
            if (query.CurrentPage <= 0)
            {
                return this.NotFound();
            }

            var userId = this.User.GetId();

            if (!this.dealersService.IsDealer(userId))
            {
                return this.RedirectToAction("Create", "Dealer");
            }

            var dealerId = this.GetUserId();
            var motorcycleResult = await this.motorcycleService.GetCollectionOfMotorsAsync(query.CurrentPage, AllMotorcyclesQueryModel.MotorcyclesPerPage, dealerId);
            query.TotalMotorcycle = motorcycleResult.TotalMotorcycles;
            query.Motors = motorcycleResult.Motorcycle;
            return this.View(query);
        }

        [Authorize]
        public async Task<IActionResult> Offer(int id)
        {
            var userId = this.User.GetId();
            var model = await this.motorcycleService.GetMotorcycleByIdAsync(id);
            if (model.DealerId != userId)
            {
                return this.BadRequest();
            }

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Offer(bool available, int id)
        {
            var userId = this.User.GetId();

            await this.motorcycleService.ChangeStateOfMotorcycle(available, id);

            var model = await this.motorcycleService.GetMotorcycleByIdAsync(id);

            if (model.DealerId != userId)
            {
                return this.BadRequest();
            }

            if (available)
            {
                this.TempData["Message"] = "The bike is available";
            }
            else
            {
                this.TempData["Message"] = "The bike is unavailabe";
            }

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> OfferThisModel(int id)
        {
            var userId = this.User.GetId();

            var motor = new OfferThisModelForm
            {
                Motor = await this.motorcycleService.GetMotorcycleByIdAsync(id),
                PickUpDate = DateTime.UtcNow,
                DropOffDate = DateTime.UtcNow.AddDays(1),
            };

            if (motor.Motor.DealerId != userId)
            {
                return this.BadRequest();
            }

            return this.View(motor);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> OfferThisModel(OfferThisModelForm offerThisModelForm, [FromRoute] int id)
        {
            var pickUpDate = offerThisModelForm.PickUpDate;
            var dropOffDate = offerThisModelForm.DropOffDate;
            var offer = new OfferPeriodForMotorDto
            {
                PickUpDate = pickUpDate,
                DropOffDate = dropOffDate,
            };
            await this.motorcycleService.OfferCurrentMotor(offer, id);

            this.TempData["Message"] = "The bike is successfully offered";

            return this.RedirectToAction("All", "Motor");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.dealersService.IsDealer(userId))
            {
                return this.RedirectToAction("Create", "Dealer");
            }

            var motor = this.motorcycleService.DetailsForEdit(id);
            if (motor.DealerId != userId)
            {
                return this.BadRequest();
            }

            return this.View(new MotorcycleFormModel
            {
                Manufacturer = motor.Manufacturer,
                Model = motor.Model,
                Type = (MotorType)motor.Type,
                Year = motor.Year,
                Country = motor.Country,
                City = motor.City,
                Color = motor.Color,
                Available = motor.Available,
                CubicCentimetre = motor.CubicCentimetre,
                Description = motor.Description,
                Price = motor.Price,
                Url = motor.Url,
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(MotorcycleServiceDto motorcycleFormModel)
        {
            var userId = this.User.GetId();
            var dealerId = this.dealersService.GetDealerId(userId);

            if (!this.dealersService.IsDealer(userId))
            {
                return this.RedirectToAction("Create", "Dealer");
            }

            if (!this.motorcycleService.IsByDealer(motorcycleFormModel.Id, dealerId))
            {
                return this.BadRequest();
            }

            var motor = this.motorcycleService.DetailsForEdit(motorcycleFormModel.Id);
            if (motor.DealerId != userId)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(new MotorcycleFormModel
                {
                    Manufacturer = motor.Manufacturer,
                    Model = motor.Model,
                    Type = (MotorType)motor.Type,
                    Year = motor.Year,
                    Country = motor.Country,
                    City = motor.City,
                    Color = motor.Color,
                    Available = motor.Available,
                    CubicCentimetre = motor.CubicCentimetre,
                    Description = motor.Description,
                    Price = motor.Price,
                    Url = motor.Url,
                });
            }

            var isMotorcycleEdited = this.motorcycleService.Edit(motorcycleFormModel, motorcycleFormModel.Id);

            if (!isMotorcycleEdited.Result)
            {
                return this.BadRequest();
            }

            this.TempData["EditSuccessful"] = "Motorcycles edited successful!";

            return this.RedirectToAction("All", "Motor");
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

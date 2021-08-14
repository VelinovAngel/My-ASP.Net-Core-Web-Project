namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;
    using BikesBooking.Services.Data.Dealer;
    using BikesBooking.Services.Data.DTO.Dealers;
    using BikesBooking.Services.Data.DTO.MotorcycleModels;
    using BikesBooking.Services.Data.Motorcycle;
    using BikesBooking.Web.Infrastructure;
    using BikesBooking.Web.ViewModels.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        private readonly IMotorcycleService motorcycleService;
        private readonly IDealersService dealersService;

        public UserController(
            IMotorcycleService motorcycleService,
            IDealersService dealersService)
        {
            this.motorcycleService = motorcycleService;
            this.dealersService = dealersService;
        }

        public async Task<IActionResult> FreeMotors([FromQuery] AllFreeMotorcylesQueryDto query, SearchMotorcycleInputModel inputModel)
        {
            var motorcycleResult = await this.motorcycleService.GetFreeMotors(query.CurrentPage, AllFreeMotorcylesQueryDto.MotorcyclesPerPage, inputModel);
            query.TotalMotorcycle = motorcycleResult.TotalMotorcycles;
            query.Motors = motorcycleResult.Motorcycle;
            return this.View(query);
        }

        [Authorize(Roles = "Dealer")]
        public IActionResult EditDealerProfil()
        {
            var userId = this.User.GetId();
            var dealerId = this.dealersService.GetDealerIdByUser(userId);
            var intDeaelerId = this.dealersService.GetDealerId(userId);
            var dealer = this.dealersService.GetCurrentDealerInfo(intDeaelerId);

            if (userId != dealerId)
            {
                return this.BadRequest();
            }

            return this.View(new BecomeDealerFormModel
            {
                Name = dealer.Name,
                Address = dealer.Address,
                Country = dealer.Country,
                City = dealer.City,
                Description = dealer.Description,
                Email = dealer.Email,
            });
        }

        [Authorize(Roles = "Dealer")]
        [HttpPost]
        public async Task<IActionResult> EditDealerProfil(CreateDealerDto dealer)
        {
            var userId = this.User.GetId();
            var dealerId = this.dealersService.GetDealerIdByUser(userId);
            var intDeaelerId = this.dealersService.GetDealerId(userId);

            if (userId != dealerId)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(new BecomeDealerFormModel
                {
                    Name = dealer.Name,
                    Address = dealer.Address,
                    Country = dealer.Country,
                    City = dealer.City,
                    Description = dealer.Description,
                    Email = dealer.Email,
                });
            }

            var isDealerEdited = await this.dealersService.Edit(dealer, intDeaelerId);

            if (!isDealerEdited)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Client")]
        public IActionResult EditClientProfil()
        {
            return this.View();
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public IActionResult EditClientProfil(string address, string city)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return this.View();
        }
    }
}

namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.Client;
    using BikesBooking.Services.Data.Dealer;
    using BikesBooking.Services.Data.DTO.Dealers;
    using BikesBooking.Services.Data.DTO.MotorcycleModels;
    using BikesBooking.Services.Data.Motorcycle;
    using BikesBooking.Services.Services;
    using BikesBooking.Web.Infrastructure;
    using BikesBooking.Web.ViewModels.Client;
    using BikesBooking.Web.ViewModels.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        private readonly IMotorcycleService motorcycleService;
        private readonly IDealersService dealersService;
        private readonly IClientService clientService;
        private readonly ICloudinaryService cloudinaryService;

        public UserController(
            IMotorcycleService motorcycleService,
            IDealersService dealersService,
            IClientService clientService,
            ICloudinaryService cloudinaryService)
        {
            this.motorcycleService = motorcycleService;
            this.dealersService = dealersService;
            this.clientService = clientService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> FreeMotors([FromQuery] AllFreeMotorcylesQueryDto query, SearchMotorcycleInputModel inputModel)
        {
            var motorcycleResult = await this.motorcycleService.GetFreeMotors(query.CurrentPage, AllFreeMotorcylesQueryDto.MotorcyclesPerPage, inputModel);
            query.TotalMotorcycle = motorcycleResult.TotalMotorcycles;
            query.Motors = motorcycleResult.Motorcycle;
            return this.View(query);
        }

        [Authorize(Roles = "Dealer")]
        public IActionResult EditDealerProfile()
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
                ImageFile = dealer.ImageFile,
            });
        }

        [Authorize(Roles = "Dealer")]
        [HttpPost]
        public async Task<IActionResult> EditDealerProfile(CreateDealerDto dealer)
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

            var imageUrl = await this.cloudinaryService.UploudAsync(dealer.ImageFile);

            var isDealerEdited = await this.dealersService.Edit(dealer, intDeaelerId, imageUrl);

            if (!isDealerEdited)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Client")]
        public IActionResult EditClientProfile()
        {
            var userId = this.User.GetId();
            var clientId = this.clientService.GetClientIdByUser(userId);
            var intClientId = this.clientService.GetClientId(userId);
            var clientInfo = this.clientService.GetCurrentClientInfo(intClientId);

            if (userId != clientId)
            {
                return this.BadRequest();
            }

            var tokens = clientInfo.Split(" - ");

            return this.View(new BecomeClientFromModel
            {
                City = tokens[0],
                Address = tokens[1],
            });
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> EditClientProfile(string address, string city)
        {
            var userId = this.User.GetId();
            var clientId = this.clientService.GetClientIdByUser(userId);
            var intClientId = this.clientService.GetClientId(userId);
            var clientInfo = this.clientService.GetCurrentClientInfo(intClientId);

            if (userId != clientId)
            {
                return this.BadRequest();
            }

            var tokens = clientInfo.Split(" - ");

            if (!this.ModelState.IsValid)
            {
                return this.View(new BecomeClientFromModel
                {
                    City = tokens[0],
                    Address = tokens[1],
                });
            }

            var isClientEdited = await this.clientService.Edit(intClientId, address, city);

            if (!isClientEdited)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}

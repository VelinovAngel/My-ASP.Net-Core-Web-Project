namespace BikesBooking.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BikesBooking.Common;
    using BikesBooking.Services.Data.Client;
    using BikesBooking.Services.Data.Motorcycle;
    using BikesBooking.Services.Data.User;
    using BikesBooking.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ClientController : Controller
    {
        private readonly IClientService clientService;
        private readonly IUserService userService;
        private readonly IMotorcycleService motorcycleService;
        private readonly IServiceProvider serviceProvider;

        public ClientController(
            IClientService clientService,
            IUserService userService,
            IMotorcycleService motorcycleService,
            IServiceProvider serviceProvider)
        {
            this.clientService = clientService;
            this.userService = userService;
            this.motorcycleService = motorcycleService;
            this.serviceProvider = serviceProvider;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string address, string city)
        {
            var userId = this.User.GetId();
            var isAlreadyExists = this.clientService.IsAlreadyClientExist(userId);

            if (isAlreadyExists)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.clientService.CreateClientAsync(userId, address, city);

            var email = this.clientService.GetCurrentClientEmail(userId);
            await this.userService.AssignRole(this.serviceProvider, email, GlobalConstants.ClientRoleName);

            return this.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Details(int id, string information)
        {
            var model = this.motorcycleService.Details(id);

            if (information != model.ToFriendlyUrl())
            {
                return this.BadRequest();
            }

            return this.View(model);
        }

        [Authorize]
        public IActionResult Book(int id)
        {
            var userId = this.User.GetId();

            var currClient = this.clientService.GetCurrentClient(userId);

            if (currClient.UserId != userId)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("BookThisModel", "Client");
        }

        [Authorize]
        public IActionResult BookThisModel(int id, string information)
        {
            var userId = this.User.GetId();

            var currClient = this.clientService.GetCurrentClient(userId);

            if (currClient.UserId != userId)
            {
                return this.BadRequest();
            }

            var model = this.motorcycleService.Details(id);
            if (information != model.ToFriendlyUrl())
            {
                return this.BadRequest();
            }

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BookThisModel(int id, DateTime oldPickUpData, DateTime oldDropOffData, DateTime pickUpDate, DateTime dropOffDate)
        {
            var userId = this.User.GetId();
            var clientId = this.clientService.GetClientId(userId);

            var currClient = this.clientService.GetCurrentClient(userId);

            if (currClient.UserId != userId)
            {
                return this.BadRequest();
            }

            var offerId = this.clientService.GetCurrentOfferId(oldPickUpData, oldDropOffData);

            await this.clientService.BookedMotorcycleByClient(clientId, offerId, pickUpDate, dropOffDate, id);

            this.TempData["Message"] = "Motorcycles successfully booked!";

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult MyAllMotorcycle()
        {
            var userId = this.User.GetId();
            var clientId = this.clientService.GetClientId(userId);
            var currClient = this.clientService.GetCurrentClient(userId);

            if (currClient.UserId != userId)
            {
                return this.BadRequest();
            }

            var model = this.clientService.GetAllListOfMotorcycleByClietId(clientId);

            return this.View(model);
        }

        [Authorize]
        public IActionResult DetailsByMotorcycleId(int id)
        {
            var userId = this.User.GetId();
            var clientId = this.clientService.GetClientId(userId);
            var currClient = this.clientService.GetCurrentClient(userId);

            if (currClient.UserId != userId)
            {
                return this.BadRequest();
            }

            var model = this.clientService.GetSingleBookedMotorcycleByClientId(clientId, id);

            return this.View(model);
        }
    }
}

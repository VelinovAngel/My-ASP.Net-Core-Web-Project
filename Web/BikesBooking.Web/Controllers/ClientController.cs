namespace BikesBooking.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BikesBooking.Common;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.Client;
    using BikesBooking.Services.Data.Motorcycle;
    using BikesBooking.Services.Data.User;
    using BikesBooking.Services.Data.Votes;
    using BikesBooking.Web.Areas.Identity.Pages.Account;
    using BikesBooking.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ClientController : Controller
    {
        private readonly IClientService clientService;
        private readonly IUserService userService;
        private readonly IMotorcycleService motorcycleService;
        private readonly IServiceProvider serviceProvider;
        private readonly IVoteService votesService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LogoutModel> logger;

        public ClientController(
            IClientService clientService,
            IUserService userService,
            IMotorcycleService motorcycleService,
            IServiceProvider serviceProvider,
            IVoteService votesService,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
        {
            this.clientService = clientService;
            this.userService = userService;
            this.motorcycleService = motorcycleService;
            this.serviceProvider = serviceProvider;
            this.votesService = votesService;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [Authorize]
        public IActionResult Create()
        {
            if (this.User.IsInRole("Dealer"))
            {
                return this.BadRequest();
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string address, string city)
        {
            var userId = this.User.GetId();
            var isAlreadyExists = this.clientService.IsAlreadyClientExist(userId);

            if (this.User.IsInRole("Client"))
            {
                return this.BadRequest();
            }

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

            await this.signInManager.SignOutAsync();
            this.logger.LogInformation("User logged out.");

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

            await this.clientService.BookThisMotorcycleByClient(clientId, offerId, pickUpDate, dropOffDate, id);

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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Reviews(int id, string descriptionMessage)
        {
            var userId = this.User.GetId();
            var clientName = this.clientService.GetCurrentClient(userId);

            var vote = this.votesService.GetVoteByUser(userId);

            await this.clientService.CreaterReviewByUser(id, (byte)vote, clientName.Name, descriptionMessage);

            this.TempData["message"] = "You have successfully submitted a review for this motorcycle. Thank you!";
            return this.RedirectToAction("DetailsByMotorcycleId", new { id = id });
        }
    }
}

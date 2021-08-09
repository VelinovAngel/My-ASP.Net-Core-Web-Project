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

        public IActionResult Details(int id, string information)
        {
            var model = this.motorcycleService.Details(id);

            if (information != model.ToFriendlyUrl())
            {
                return this.BadRequest();
            }

            return this.View(model);
        }

        public IActionResult Book()
        {
            return this.View();
        }
    }
}

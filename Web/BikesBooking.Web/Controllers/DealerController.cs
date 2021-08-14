namespace BikesBooking.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BikesBooking.Common;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.Dealer;
    using BikesBooking.Services.Data.DTO.Dealers;
    using BikesBooking.Services.Data.User;
    using BikesBooking.Web.Areas.Identity.Pages.Account;
    using BikesBooking.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class DealerController : BaseController
    {
        private readonly IDealersService dealersService;
        private readonly IUserService userService;
        private readonly IServiceProvider serviceProvider;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LogoutModel> logger;

        public DealerController(
            IDealersService dealersService,
            IUserService userService,
            IServiceProvider serviceProvider,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
        {
            this.dealersService = dealersService;
            this.userService = userService;
            this.serviceProvider = serviceProvider;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateDealerDto dealer)
        {
            var userId = this.User.GetId();
            var isAlreadyExistId = this.dealersService.IsDealer(userId);

            if (this.User.IsInRole("Dealer"))
            {
                return this.BadRequest();
            }

            if (isAlreadyExistId)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.dealersService.CreateDealerAsync(dealer, userId);

            await this.userService.AssignRole(this.serviceProvider, dealer.Email, GlobalConstants.DealerRoleName);

            await this.signInManager.SignOutAsync();
            this.logger.LogInformation("User logged out.");

            return this.RedirectToAction("Index", "Home");
        }
    }
}

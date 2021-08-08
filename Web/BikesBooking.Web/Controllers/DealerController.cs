namespace BikesBooking.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BikesBooking.Common;
    using BikesBooking.Services.Data.Dealer;
    using BikesBooking.Services.Data.DTO.Dealers;
    using BikesBooking.Services.Data.User;
    using BikesBooking.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DealerController : BaseController
    {
        private readonly IDealersService dealersService;
        private readonly IUserService userService;
        private readonly IServiceProvider serviceProvider;

        public DealerController(
            IDealersService dealersService,
            IUserService userService,
            IServiceProvider serviceProvider)
        {
            this.dealersService = dealersService;
            this.userService = userService;
            this.serviceProvider = serviceProvider;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateDealerDto dealer)
        {
            var userId = this.User.GetId();
            var isAlreadyExistId = this.dealersService.IsDealer(userId);

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

            this.TempData["AddDealerSuccessful"] = "Added new dealer successfully";

            return this.RedirectToAction("Index", "Home");
        }
    }
}

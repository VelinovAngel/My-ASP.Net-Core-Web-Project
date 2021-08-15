namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.Dealer;
    using BikesBooking.Services.Data.DTO.Dealers;
    using BikesBooking.Services.Services;
    using BikesBooking.Web.Areas.Identity.Pages.Account;
    using BikesBooking.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class DealerController : BaseController
    {
        private readonly IDealersService dealersService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LogoutModel> logger;
        private readonly ICloudinaryService cloudinaryService;

        public DealerController(
            IDealersService dealersService,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger,
            ICloudinaryService cloudinaryService)
        {
            this.dealersService = dealersService;
            this.signInManager = signInManager;
            this.logger = logger;
            this.cloudinaryService = cloudinaryService;
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

            var imageUrl = await this.cloudinaryService.UploudAsync(dealer.ImageFile);

            await this.dealersService.CreateDealerAsync(dealer, userId, imageUrl);

            await this.signInManager.SignOutAsync();

            this.logger.LogInformation("User logged out.");

            return this.RedirectToAction("Index", "Home");
        }
    }
}

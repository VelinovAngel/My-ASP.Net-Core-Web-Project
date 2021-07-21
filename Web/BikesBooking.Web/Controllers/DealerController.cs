namespace BikesBooking.Web.Controllers
{
    using BikesBooking.Services.Data;
    using BikesBooking.Services.Data.DTO.Dealers;
    using BikesBooking.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DealerController : BaseController
    {
        private readonly IDealersService dealersService;

        public DealerController(IDealersService dealersService)
        {
            this.dealersService = dealersService;
        }

        public IActionResult CreatePublic()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePublic(CreateDealerDto publicDealer)
        {
            var userId = this.User.GetId();
            var isAlreadyExistId = this.dealersService.IsAlreadyPublicDealerExist(userId);

            if (isAlreadyExistId)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.dealersService.CreatePublicDealer(publicDealer, userId);

            this.TempData["AddDealerSuccessful"] = "Added new dealer successfully";

            return this.RedirectToAction("All", "Motor");
        }

        public IActionResult CreatePrivate()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePrivate(CreateDealerDto privateDealer)
        {
            return this.View();
        }
    }
}

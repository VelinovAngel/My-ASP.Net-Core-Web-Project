namespace BikesBooking.Web.Controllers
{
    using BikesBooking.Services.Data;
    using BikesBooking.Web.Infrastructure;
    using BikesBooking.Web.ViewModels.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DealerController : BaseController
    {
        private readonly IDealersService dealersService;

        public DealerController(IDealersService dealersService)
        {
            this.dealersService = dealersService;
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePublic(BecomeDealerFormModel publicDealer)
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePrivate(BecomeDealerFormModel privateDealer)
        {
            var userId = this.User.GetId();
            var IsAlreadyExistId = this.dealersService.IsAlreadyPublicDealerExist(userId);

            if (IsAlreadyExistId)
            {
                return this.BadRequest();
            }

            return this.View();
        }
    }
}

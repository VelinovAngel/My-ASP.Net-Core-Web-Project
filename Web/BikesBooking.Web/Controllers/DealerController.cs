namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;

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

        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateDealerDto publicDealer)
        {
            var userId = this.User.GetId();
            var isAlreadyExistId = this.dealersService.IsAlreadyDealerExist(userId);

            if (isAlreadyExistId)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.dealersService.CreateDealerAsync(publicDealer, userId);

            this.TempData["AddDealerSuccessful"] = "Added new dealer successfully";

            return this.RedirectToAction("All", "Motor");
        }
    }
}

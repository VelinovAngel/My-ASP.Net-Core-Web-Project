namespace BikesBooking.Web.Controllers
{
    using BikesBooking.Services.Data.Motorcycle;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ClientController : Controller
    {
        private readonly IMotorcycleService motorcycleService;

        public ClientController(IMotorcycleService motorcycleService)
        {
            this.motorcycleService = motorcycleService;
        }

        public IActionResult Book()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var model = this.motorcycleService.Details(id);
            return this.View(model);
        }
    }
}

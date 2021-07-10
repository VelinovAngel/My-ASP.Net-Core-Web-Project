namespace BikesBooking.Controllers
{
    using System;
    using System.Diagnostics;

    using BikesBooking.Models;
    using BikesBooking.Models.Motor;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            var model = new SearchMotorModel
            {
                DropOffDate = DateTime.UtcNow,
                PickUpDate = DateTime.UtcNow.AddDays(1),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SearchMotorModel model)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

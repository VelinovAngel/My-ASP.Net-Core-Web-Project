namespace BikesBooking.Web.Controllers
{
    using System;
    using System.Diagnostics;

    using BikesBooking.Services.Data.Home;
    using BikesBooking.Web.ViewModels;
    using BikesBooking.Web.ViewModels.Motor;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 30)]
        public IActionResult Index()
        {
            var viewModel = new SearchFormMotorModel
            {
                PickUpDate = DateTime.UtcNow,
                DropOffDate = DateTime.UtcNow.AddDays(1),
            };
            viewModel.CityCount = this.homeService.GetAllCityCount();
            viewModel.CountriesItems = this.homeService.GetKeyValuePairsCoutries();
            viewModel.ManufacturerItems = this.homeService.GetKeyValuePairsModels();
            return this.View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(SearchFormMotorModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CityCount = this.homeService.GetAllCityCount();
                input.CountriesItems = this.homeService.GetKeyValuePairsCoutries();
                input.ManufacturerItems = this.homeService.GetKeyValuePairsModels();
                return this.View(input);
            }

            return this.RedirectToAction("FreeMotors", "User", input);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

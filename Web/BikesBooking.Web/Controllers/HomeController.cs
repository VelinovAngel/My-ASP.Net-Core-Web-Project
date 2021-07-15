namespace BikesBooking.Web.Controllers
{
    using System.Diagnostics;

    using BikesBooking.Services.Data;
    using BikesBooking.Web.ViewModels;
    using BikesBooking.Web.ViewModels.Motor;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchMotorModel();
            viewModel.CountriesItems = this.homeService.GetKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(SearchMotorModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.homeService.GetKeyValuePairs();
                return this.View(input);
            }

            return this.Redirect("/");
        }

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

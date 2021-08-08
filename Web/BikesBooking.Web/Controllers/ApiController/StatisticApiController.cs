namespace BikesBooking.Web.Controllers.ApiController
{
    using BikesBooking.Services.Data.Motorcycle;
    using BikesBooking.Services.Data.User;
    using BikesBooking.Web.ViewModels.Api.Statistics;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticApiController : ControllerBase
    {
        private readonly IMotorcycleService motorcycleService;
        private readonly IUserService userService;

        public StatisticApiController(
            IMotorcycleService motorcycleService,
            IUserService userService)
        {
            this.motorcycleService = motorcycleService;
            this.userService = userService;
        }

        [HttpGet]
        public StatisticsResponseModel GetStatistics()
        {
            var totalMotorcycles = this.motorcycleService.GetMotorcycleCount();
            var totalDealers = this.userService.GetTotalDeales();
            var totalUsers = this.userService.GetTotalUsers();
            var totalClients = this.userService.GetTotalClients();
            var totalRent = this.motorcycleService.GetNotAvailableMotorcycleCount();
            return new StatisticsResponseModel
            {
                TotalsMotorcycles = totalMotorcycles,
                TotalsUsers = totalUsers,
                TotalsRent = totalRent,
                TotalsDealers = totalDealers,
                TotalsClients = totalClients,
            };
        }
    }
}

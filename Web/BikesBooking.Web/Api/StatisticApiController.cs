namespace BikesBooking.Web.Api
{
    using BikesBooking.Services.Data;
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
            var totalRent = this.userService.GetTotalUsers();
            return new StatisticsResponseModel
            {
                TotalsMotorcycles = totalMotorcycles,
                TotalsRent = 0,
                TotalsUsers = totalRent,
            };
        }
    }
}

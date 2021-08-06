namespace BikesBooking.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using BikesBooking.Common;
    using BikesBooking.Services.Data.Dealer;
    using BikesBooking.Services.Data.DTO.Dealers;
    using BikesBooking.Services.Data.User;
    using BikesBooking.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DealerController : BaseController
    {
        private readonly IDealersService dealersService;
        private readonly IUserService userService;
        private readonly IServiceProvider serviceProvider;

        public DealerController(
            IDealersService dealersService,
            IUserService userService,
            IServiceProvider serviceProvider)
        {
            this.dealersService = dealersService;
            this.userService = userService;
            this.serviceProvider = serviceProvider;
        }
    }
}

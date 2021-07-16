namespace BikesBooking.Web.Controllers
{
    using BikesBooking.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class MotorController : Controller
    {
        public IActionResult Add()
        {
            return this.View();
        }
    }
}

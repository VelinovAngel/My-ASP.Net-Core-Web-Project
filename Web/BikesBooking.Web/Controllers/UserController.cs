namespace BikesBooking.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        public IActionResult Dealer()
        {
            return this.View();
        }

        public IActionResult Client()
        {
            return this.View();
        }

        public IActionResult DealerOrClient()
        {
            return this.View();
        }

        public IActionResult FreeMotors()
        {
            return this.View();
        }
    }
}

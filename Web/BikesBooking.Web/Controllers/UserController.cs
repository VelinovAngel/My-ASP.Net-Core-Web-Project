namespace BikesBooking.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        public IActionResult FreeMotors()
        {
            return this.View();
        }
    }
}

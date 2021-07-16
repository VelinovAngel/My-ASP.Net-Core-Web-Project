namespace BikesBooking.Web.Controllers
{
    using BikesBooking.Web.ViewModels.Motor;
    using Microsoft.AspNetCore.Mvc;

    public class MotorController : Controller
    {
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddMotorcycleModel motorcycle)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.TempData["Message"] = "Motorcycles added successful!";

            return this.Redirect("/Motor/All");
        }

        public IActionResult All()
        {
            return this.View();
        }

        public IActionResult FreeMotors()
        {
            return this.View();
        }
    }
}

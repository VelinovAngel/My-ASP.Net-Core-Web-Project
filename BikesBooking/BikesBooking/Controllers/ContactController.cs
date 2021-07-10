namespace BikesBooking.Controllers
{
    using BikesBooking.Models.Contact;
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactUsForm contact)
        {
            return View();
        }
    }
}

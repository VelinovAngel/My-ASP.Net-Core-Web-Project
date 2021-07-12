namespace BikesBooking.Web.Controllers
{
    using BikesBooking.Web.ViewModels.Contact;
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : BaseController
    {
        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Contact(ContactUsForm contact)
        {
            return this.View();
        }
    }
}

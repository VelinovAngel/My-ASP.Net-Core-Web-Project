namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;
    using BikesBooking.Services.Data;
    using BikesBooking.Web.ViewModels.Contact;
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : BaseController
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactUsForm contact)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.contactService.SendContactMessageAsync(contact);

            this.TempData["Successful Message"] = "Messager sedn successfully";

            return this.RedirectToAction("Contact", "Contact");
        }
    }
}

namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data;
    using BikesBooking.Services.Data.DTO.ContactModels;
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

            var massage = new ContactFormDto
            {
                Name = contact.Name,
                Email = contact.Email,
                Description = contact.Description,
                Subject = contact.Subject,
            };

            await this.contactService.SendContactMessageAsync(massage);

            this.TempData["Successful Message"] = "Message send successfully";

            return this.RedirectToAction("Contact", "Contact");
        }
    }
}

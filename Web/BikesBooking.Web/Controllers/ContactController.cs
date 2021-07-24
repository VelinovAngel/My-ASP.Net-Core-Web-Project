namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data;
    using BikesBooking.Services.Data.DTO.ContactModels;
    using BikesBooking.Web.Infrastructure;
    using BikesBooking.Web.ViewModels.Contact;
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : BaseController
    {
        private readonly IContactService contactService;
        private readonly IDealersService dealersService;

        public ContactController(
            IContactService contactService,
            IDealersService dealersService)
        {
            this.contactService = contactService;
            this.dealersService = dealersService;
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

        public IActionResult ClientMessages()
        {
            var messages = this.contactService.GetAllMessages();
            if (messages == null)
            {
                this.TempData["NoMessages"] = "Not found message from clients!";
                return this.RedirectToAction("Contact", "Contact");
            }

            return this.View(messages.Result);
        }

        public IActionResult Details(int id)
        {
            var message = this.contactService.GetSingleMessage(id);
            if (message == null)
            {
                return this.NotFound();
            }

            return this.View(message);
        }

        public IActionResult SendEmailToUser([FromRoute] int id)
        {
            var client = new SendEmailForm();
            var currUser = this.contactService.GetInfoFromUser(id);
            client.ClientName = currUser.Username;
            return this.View(client);
        }

        [HttpPost]
        public IActionResult SendEmailToUser([FromRoute]int id, SendEmailForm emailInfo)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var currUser = this.contactService.GetInfoFromUser(id);

            var currDealerId = this.User.GetId();
            var userId = this.dealersService.GetDealerId(currDealerId);

            var dealerEmail = this.dealersService.GetCurrentDealerEmail(userId);
            var currUserEmail = currUser.Email;
            var content = emailInfo.Content;
            var subject = emailInfo.Subject;

            this.TempData["Successful Message"] = "Message send successfully";

            return this.View();
        }
    }
}

namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.Contact;
    using BikesBooking.Services.Data.Dealer;
    using BikesBooking.Services.Data.DTO.ContactModels;
    using BikesBooking.Services.Messaging;
    using BikesBooking.Web.Infrastructure;
    using BikesBooking.Web.ViewModels.Contact;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Administrator")]
    public class ContactController : BaseController
    {
        private readonly IContactService contactService;
        private readonly IDealersService dealersService;
        private readonly IEmailSenderService emailSenderService;

        public ContactController(
            IContactService contactService,
            IDealersService dealersService,
            IEmailSenderService emailSenderService)
        {
            this.contactService = contactService;
            this.dealersService = dealersService;
            this.emailSenderService = emailSenderService;
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Contact(ContactUsForm contact)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var massage = new ContactFormDto
            {
                Username = contact.Username,
                Email = contact.Email,
                Description = contact.Description,
                Complaint = contact.Complaint,
            };

            await this.contactService.SendContactMessageAsync(massage);

            this.TempData["Successful Message"] = "Message send successfully";

            return this.RedirectToAction("Contact", "Contact");
        }

        [Authorize(Roles = "Dealer")]
        public IActionResult ClientMessages()
        {
            var messages = this.contactService.GetAllMessages();
            if (messages == null)
            {
                this.TempData["NoMessages"] = "Not found message from clients!";
                return this.RedirectToAction("Contact", "Contact");
            }

            return this.View(messages);
        }

        [Authorize(Roles = "Dealer")]
        public IActionResult Details(int id)
        {
            var message = this.contactService.GetSingleMessage(id);
            if (message == null)
            {
                return this.NotFound();
            }

            return this.View(message);
        }

        [Authorize(Roles = "Dealer")]
        public IActionResult SendEmailToUser([FromRoute] int id)
        {
            var client = new SendEmailForm();
            var currUser = this.contactService.GetInfoFromUser(id);
            client.ClientName = currUser.Username;
            return this.View(client);
        }

        [Authorize(Roles = "Dealer")]
        [HttpPost]
        public IActionResult SendEmailToUser([FromRoute]int id, string subject, string content)
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
            var currContent = content;
            var currSubject = subject;

            this.emailSenderService.SendMail(dealerEmail, currUserEmail, currSubject, currContent);
            this.TempData["Successful Message"] = $"Email to {currUserEmail} send successfully";

            return this.RedirectToAction("SendEmailToUser", "Contact");
        }
    }
}

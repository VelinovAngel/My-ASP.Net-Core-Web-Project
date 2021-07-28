namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.Client;
    using BikesBooking.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ClientController : Controller
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string address, string city)
        {
            var userId = this.User.GetId();
            var isAlreadyExists = this.clientService.IsAlreadyClientExist(userId);

            if (isAlreadyExists)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.clientService.CreateClientAsync(userId, address, city);

            return this.RedirectToAction("Index", "Home");
        }
    }
}

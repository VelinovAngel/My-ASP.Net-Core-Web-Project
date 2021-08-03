namespace BikesBooking.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class DashboardController : AdministrationController
    {

        public IActionResult Index()
        {
            return this.View();
        }
    }
}

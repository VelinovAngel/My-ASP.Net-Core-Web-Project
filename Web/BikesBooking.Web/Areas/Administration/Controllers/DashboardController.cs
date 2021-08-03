namespace BikesBooking.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class DashboardController : AdministrationController
    {
        private readonly ApplicationDbContext context;

        public DashboardController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.context.Motorcycles
                .Include(m => m.City)
                .Include(m => m.Color)
                .Include(m => m.Dealer)
                .Include(m => m.Manufacturer)
                .Include(m => m.Model)
                .Include(m => m.Offer)
                .Include(m => m.Review);
            return this.View(await applicationDbContext.ToListAsync());
        }
    }
}

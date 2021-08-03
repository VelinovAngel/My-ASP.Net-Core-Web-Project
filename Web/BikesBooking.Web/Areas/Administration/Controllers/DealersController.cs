namespace BikesBooking.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data;
    using BikesBooking.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class DealersController : AdministrationController
    {
        private readonly ApplicationDbContext context;

        public DealersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Dealers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.context.Dealers
                .Include(d => d.City)
                .ThenInclude(c => c.Country);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Dealers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var dealer = await this.context.Dealers
                .Include(d => d.City)
                .ThenInclude(d => d.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dealer == null)
            {
                return this.NotFound();
            }

            return this.View(dealer);
        }

        // GET: Administration/Dealers/Create
        public IActionResult Create()
        {
            this.ViewData["CityId"] = new SelectList(this.context.Cities, "Id", "Name");
            return this.View();
        }

        // POST: Administration/Dealers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,Email,Description,CityId,DealerId,DeletedOn,Id")] Dealer dealer)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(dealer);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CityId"] = new SelectList(this.context.Cities, "Id", "Name", dealer.CityId);
            return this.View(dealer);
        }

        // GET: Administration/Dealers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var dealer = await this.context.Dealers.FindAsync(id);
            if (dealer == null)
            {
                return this.NotFound();
            }

            this.ViewData["CityId"] = new SelectList(this.context.Cities, "Id", "Name", dealer.CityId);
            return this.View(dealer);
        }

        // POST: Administration/Dealers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Address,Email,Description,CityId,CountryId,DealerId,CreatedOn")] Dealer dealer)
        {
            if (id != dealer.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(dealer);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.DealerExists(dealer.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CityId"] = new SelectList(this.context.Cities, "Id", "Name", dealer.CityId);
            return this.View(dealer);
        }

        // GET: Administration/Dealers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var dealer = await this.context.Dealers
                .Include(d => d.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dealer == null)
            {
                return this.NotFound();
            }

            return this.View(dealer);
        }

        // POST: Administration/Dealers/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dealer = await this.context.Dealers.FindAsync(id);
            this.context.Dealers.Remove(dealer);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool DealerExists(int id)
        {
            return this.context.Dealers.Any(e => e.Id == id);
        }
    }
}

namespace BikesBooking.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class DealersController : AdministrationController
    {
        private readonly IRepository<Dealer> dealer;
        private readonly IRepository<City> city;

        public DealersController(
            IRepository<Dealer> dealer,
            IRepository<City> city)
        {
            this.dealer = dealer;
            this.city = city;
        }

        // GET: Administration/Dealers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.dealer.All()
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

            var dealer = await this.dealer.All()
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
            this.ViewData["CityId"] = new SelectList(this.city.All().ToList(), "Id", "Name");
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
                await this.dealer.AddAsync(dealer);
                await this.dealer.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CityId"] = new SelectList(this.city.All().ToList(), "Id", "Name", dealer.CityId);
            return this.View(dealer);
        }

        // GET: Administration/Dealers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var dealer = this.dealer.All().FirstOrDefault(x => x.Id == id);
            if (dealer == null)
            {
                return this.NotFound();
            }

            this.ViewData["CityId"] = new SelectList(this.city.All().ToList(), "Id", "Name", dealer.CityId);
            return this.View(dealer);
        }

        // POST: Administration/Dealers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Address,Email,Description,CityId,CountryId,Id,DealerId,CreatedOn")] Dealer dealer)
        {
            if (id != dealer.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    dealer.CreatedOn = DateTime.UtcNow;
                    this.dealer.Update(dealer);
                    await this.dealer.SaveChangesAsync();
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

            this.ViewData["CityId"] = new SelectList(this.city.All().ToList(), "Id", "Name", dealer.CityId);
            return this.View(dealer);
        }

        // GET: Administration/Dealers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var dealer = await this.dealer.All()
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
            var dealer = this.dealer.All().FirstOrDefault(x => x.Id == id);
            this.dealer.Delete(dealer);
            await this.dealer.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool DealerExists(int id)
        {
            return this.dealer.All().Any(e => e.Id == id);
        }
    }
}

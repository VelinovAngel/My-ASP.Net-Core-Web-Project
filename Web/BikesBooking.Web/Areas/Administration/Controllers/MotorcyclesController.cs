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

    public class MotorcyclesController : AdministrationController
    {
        private readonly IRepository<Dealer> dealer;
        private readonly IRepository<City> city;
        private readonly IRepository<Motorcycle> motorcycle;
        private readonly IRepository<Color> color;
        private readonly IRepository<Model> model;
        private readonly IRepository<Manufacturer> manufacturer;
        private readonly IRepository<Offer> offer;
        private readonly IRepository<Review> review;

        public MotorcyclesController(
            IRepository<Dealer> dealer,
            IRepository<City> city,
            IRepository<Motorcycle> motorcycle,
            IRepository<Color> color,
            IRepository<Model> model,
            IRepository<Manufacturer> manufacturer,
            IRepository<Offer> offer,
            IRepository<Review> review)
        {
            this.dealer = dealer;
            this.city = city;
            this.motorcycle = motorcycle;
            this.color = color;
            this.model = model;
            this.manufacturer = manufacturer;
            this.offer = offer;
            this.review = review;
        }

        // GET: Administration/Motorcycles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.motorcycle.All()
                .Include(m => m.City)
                .Include(m => m.Color)
                .Include(m => m.Dealer)
                .Include(m => m.Manufacturer)
                .Include(m => m.Model)
                .Include(m => m.Offer);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Motorcycles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var motorcycle = await this.motorcycle.All()
                .Include(m => m.City)
                .Include(m => m.Color)
                .Include(m => m.Dealer)
                .Include(m => m.Manufacturer)
                .Include(m => m.Model)
                .Include(m => m.Offer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycle == null)
            {
                return this.NotFound();
            }

            return this.View(motorcycle);
        }

        // GET: Administration/Motorcycles/Create
        public IActionResult Create()
        {
            this.ViewData["CityId"] = new SelectList(this.city.All().ToList(), "Id", "Name");
            this.ViewData["ColorId"] = new SelectList(this.color.All().ToList(), "Id", "Name");
            this.ViewData["DealerId"] = new SelectList(this.dealer.All().ToList(), "Id", "Address");
            this.ViewData["ManufacturerId"] = new SelectList(this.manufacturer.All().ToList(), "Id", "Name");
            this.ViewData["ModelId"] = new SelectList(this.model.All().ToList(), "Id", "Name");
            this.ViewData["OfferId"] = new SelectList(this.offer.All().ToList(), "Id", "Id");
            return this.View();
        }

        // POST: Administration/Motorcycles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManufacturerId,ModelId,ColorId,CubicCentimetre,Description,Price,Available,TypeMotor,Url,ReviewId,CityId,OfferId,DealerId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Motorcycle motorcycle)
        {
            if (this.ModelState.IsValid)
            {
                await this.motorcycle.AddAsync(motorcycle);
                await this.motorcycle.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CityId"] = new SelectList(this.city.All().ToList(), "Id", "Name", motorcycle.CityId);
            this.ViewData["ColorId"] = new SelectList(this.color.All().ToList(), "Id", "Name", motorcycle.ColorId);
            this.ViewData["DealerId"] = new SelectList(this.dealer.All().ToList(), "Id", "Address", motorcycle.DealerId);
            this.ViewData["ManufacturerId"] = new SelectList(this.manufacturer.All().ToList(), "Id", "Name", motorcycle.ManufacturerId);
            this.ViewData["ModelId"] = new SelectList(this.model.All().ToList(), "Id", "Name", motorcycle.ModelId);
            this.ViewData["OfferId"] = new SelectList(this.offer.All().ToList(), "Id", "Id", motorcycle.OfferId);

            return this.View(motorcycle);
        }

        // GET: Administration/Motorcycles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var motorcycle = await this.motorcycle.All().FirstOrDefaultAsync(x => x.Id == id);
            if (motorcycle == null)
            {
                return this.NotFound();
            }

            this.ViewData["CityId"] = new SelectList(this.city.All().ToList(), "Id", "Name", motorcycle.CityId);
            this.ViewData["ColorId"] = new SelectList(this.color.All().ToList(), "Id", "Name", motorcycle.ColorId);
            this.ViewData["DealerId"] = new SelectList(this.dealer.All().ToList(), "Id", "Address", motorcycle.DealerId);
            this.ViewData["ManufacturerId"] = new SelectList(this.manufacturer.All().ToList(), "Id", "Name", motorcycle.ManufacturerId);
            this.ViewData["ModelId"] = new SelectList(this.model.All().ToList(), "Id", "Name", motorcycle.ModelId);
            this.ViewData["OfferId"] = new SelectList(this.offer.All().ToList(), "Id", "Id", motorcycle.OfferId);

            return this.View(motorcycle);
        }

        // POST: Administration/Motorcycles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManufacturerId,ModelId,ColorId,CubicCentimetre,Description,Price,Available,IsApproved,TypeMotor,Url,ReviewId,CityId,OfferId,DealerId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Motorcycle motorcycle)
        {
            if (id != motorcycle.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    motorcycle.CreatedOn = DateTime.UtcNow;
                    this.motorcycle.Update(motorcycle);
                    await this.motorcycle.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.MotorcycleExists(motorcycle.Id))
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

            this.ViewData["CityId"] = new SelectList(this.city.All().ToList(), "Id", "Name", motorcycle.CityId);
            this.ViewData["ColorId"] = new SelectList(this.color.All().ToList(), "Id", "Name", motorcycle.ColorId);
            this.ViewData["DealerId"] = new SelectList(this.dealer.All().ToList(), "Id", "Address", motorcycle.DealerId);
            this.ViewData["ManufacturerId"] = new SelectList(this.manufacturer.All().ToList(), "Id", "Name", motorcycle.ManufacturerId);
            this.ViewData["ModelId"] = new SelectList(this.model.All().ToList(), "Id", "Name", motorcycle.ModelId);
            this.ViewData["OfferId"] = new SelectList(this.offer.All().ToList(), "Id", "Id", motorcycle.OfferId);
            return this.View(motorcycle);
        }

        // GET: Administration/Motorcycles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var motorcycle = await this.motorcycle.All()
                .Include(m => m.City)
                .Include(m => m.Color)
                .Include(m => m.Dealer)
                .Include(m => m.Manufacturer)
                .Include(m => m.Model)
                .Include(m => m.Offer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycle == null)
            {
                return this.NotFound();
            }

            return this.View(motorcycle);
        }

        // POST: Administration/Motorcycles/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motorcycle = await this.motorcycle.All().FirstOrDefaultAsync(x => x.Id == id);
            this.motorcycle.Delete(motorcycle);
            await this.motorcycle.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool MotorcycleExists(int id)
        {
            return this.motorcycle.All().Any(e => e.Id == id);
        }
    }
}

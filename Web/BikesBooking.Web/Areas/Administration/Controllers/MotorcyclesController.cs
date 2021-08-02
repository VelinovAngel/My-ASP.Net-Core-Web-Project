namespace BikesBooking.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data;
    using BikesBooking.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class MotorcyclesController : AdministrationController
    {
        private readonly ApplicationDbContext context;

        public MotorcyclesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Motorcycles
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

        // GET: Administration/Motorcycles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var motorcycle = await this.context.Motorcycles
                .Include(m => m.City)
                .Include(m => m.Color)
                .Include(m => m.Dealer)
                .Include(m => m.Manufacturer)
                .Include(m => m.Model)
                .Include(m => m.Offer)
                .Include(m => m.Review)
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
            this.ViewData["CityId"] = new SelectList(this.context.Cities, "Id", "Name");
            this.ViewData["ColorId"] = new SelectList(this.context.Colors, "Id", "Name");
            this.ViewData["DealerId"] = new SelectList(this.context.Dealers, "Id", "Address");
            this.ViewData["ManufacturerId"] = new SelectList(this.context.Manufacturers, "Id", "Name");
            this.ViewData["ModelId"] = new SelectList(this.context.Models, "Id", "Name");
            this.ViewData["OfferId"] = new SelectList(this.context.Offers, "Id", "Id");
            this.ViewData["ReviewId"] = new SelectList(this.context.Reviews, "Id", "Description");
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
                this.context.Add(motorcycle);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CityId"] = new SelectList(this.context.Cities, "Id", "Name", motorcycle.CityId);
            this.ViewData["ColorId"] = new SelectList(this.context.Colors, "Id", "Name", motorcycle.ColorId);
            this.ViewData["DealerId"] = new SelectList(this.context.Dealers, "Id", "Address", motorcycle.DealerId);
            this.ViewData["ManufacturerId"] = new SelectList(this.context.Manufacturers, "Id", "Name", motorcycle.ManufacturerId);
            this.ViewData["ModelId"] = new SelectList(this.context.Models, "Id", "Name", motorcycle.ModelId);
            this.ViewData["OfferId"] = new SelectList(this.context.Offers, "Id", "Id", motorcycle.OfferId);
            this.ViewData["ReviewId"] = new SelectList(this.context.Reviews, "Id", "Description", motorcycle.ReviewId);
            return View(motorcycle);
        }

        // GET: Administration/Motorcycles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var motorcycle = await this.context.Motorcycles.FindAsync(id);
            if (motorcycle == null)
            {
                return this.NotFound();
            }

            this.ViewData["CityId"] = new SelectList(this.context.Cities, "Id", "Name", motorcycle.CityId);
            this.ViewData["ColorId"] = new SelectList(this.context.Colors, "Id", "Name", motorcycle.ColorId);
            this.ViewData["DealerId"] = new SelectList(this.context.Dealers, "Id", "Address", motorcycle.DealerId);
            this.ViewData["ManufacturerId"] = new SelectList(this.context.Manufacturers, "Id", "Name", motorcycle.ManufacturerId);
            this.ViewData["ModelId"] = new SelectList(this.context.Models, "Id", "Name", motorcycle.ModelId);
            this.ViewData["OfferId"] = new SelectList(this.context.Offers, "Id", "Id", motorcycle.OfferId);
            this.ViewData["ReviewId"] = new SelectList(this.context.Reviews, "Id", "Description", motorcycle.ReviewId);
            return this.View(motorcycle);
        }

        // POST: Administration/Motorcycles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManufacturerId,ModelId,ColorId,CubicCentimetre,Description,Price,Available,TypeMotor,Url,ReviewId,CityId,OfferId,DealerId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Motorcycle motorcycle)
        {
            if (id != motorcycle.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(motorcycle);
                    await this.context.SaveChangesAsync();
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
            this.ViewData["CityId"] = new SelectList(this.context.Cities, "Id", "Name", motorcycle.CityId);
            this.ViewData["ColorId"] = new SelectList(this.context.Colors, "Id", "Name", motorcycle.ColorId);
            this.ViewData["DealerId"] = new SelectList(this.context.Dealers, "Id", "Address", motorcycle.DealerId);
            this.ViewData["ManufacturerId"] = new SelectList(this.context.Manufacturers, "Id", "Name", motorcycle.ManufacturerId);
            this.ViewData["ModelId"] = new SelectList(this.context.Models, "Id", "Name", motorcycle.ModelId);
            this.ViewData["OfferId"] = new SelectList(this.context.Offers, "Id", "Id", motorcycle.OfferId);
            this.ViewData["ReviewId"] = new SelectList(this.context.Reviews, "Id", "Description", motorcycle.ReviewId);
            return this.View(motorcycle);
        }

        // GET: Administration/Motorcycles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var motorcycle = await this.context.Motorcycles
                .Include(m => m.City)
                .Include(m => m.Color)
                .Include(m => m.Dealer)
                .Include(m => m.Manufacturer)
                .Include(m => m.Model)
                .Include(m => m.Offer)
                .Include(m => m.Review)
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
            var motorcycle = await this.context.Motorcycles.FindAsync(id);
            this.context.Motorcycles.Remove(motorcycle);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool MotorcycleExists(int id)
        {
            return this.context.Motorcycles.Any(e => e.Id == id);
        }
    }
}

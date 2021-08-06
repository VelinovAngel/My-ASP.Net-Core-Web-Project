namespace BikesBooking.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ClientsController : Controller
    {
        private readonly IRepository<Client> client;

        public ClientsController(IRepository<Client> client)
        {
            this.client = client;
        }

        // GET: Administration/Clients
        public async Task<IActionResult> Index()
        {
            return this.View(await this.client.All().ToListAsync());
        }

        // GET: Administration/Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var client = await this.client.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return this.NotFound();
            }

            return this.View(client);
        }

        // GET: Administration/Clients/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,Email,ClientId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Client client)
        {
            if (this.ModelState.IsValid)
            {
                await this.client.AddAsync(client);
                await this.client.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(client);
        }

        // GET: Administration/Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var client = await this.client.All().FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
            {
                return this.NotFound();
            }

            return this.View(client);
        }

        // POST: Administration/Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Address,Email,ClientId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Client client)
        {
            if (id != client.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    client.ModifiedOn = DateTime.UtcNow;
                    this.client.Update(client);
                    await this.client.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ClientExists(client.Id))
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

            return this.View(client);
        }

        // GET: Administration/Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var client = await this.client.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return this.NotFound();
            }

            return this.View(client);
        }

        // POST: Administration/Clients/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await this.client.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            this.client.Delete(client);
            await this.client.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ClientExists(int id)
        {
            return this.client.All().Any(e => e.Id == id);
        }
    }
}

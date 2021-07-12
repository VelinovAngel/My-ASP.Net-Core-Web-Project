namespace BikesBooking.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Models;

    public class ModelMotorcycleSeeder : ISeeder
    {
        private readonly ApplicationDbContext db;

        public ModelMotorcycleSeeder(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (this.db.Models.Any())
            {
                return;
            }

            await this.db.Models.AddAsync(new Model { Name = "Tracer 700GT" });
            await this.db.Models.AddAsync(new Model { Name = "Tracer 900GT" });
            await this.db.Models.AddAsync(new Model { Name = "Multistrada 1260" });
            await this.db.Models.AddAsync(new Model { Name = "Diavel" });
            await this.db.Models.AddAsync(new Model { Name = "Downtown 300i" });
            await this.db.Models.AddAsync(new Model { Name = "Mt09 SP" });
            await this.db.Models.AddAsync(new Model { Name = "NC750X" });
            await this.db.Models.AddAsync(new Model { Name = "S1000RR" });
            await this.db.Models.AddAsync(new Model { Name = "ZX6R Ninja" });
            await this.db.Models.AddAsync(new Model { Name = "Z900" });
            await this.db.Models.AddAsync(new Model { Name = "CB500X" });
            await this.db.Models.AddAsync(new Model { Name = "GS1250R" });
            await this.db.Models.AddAsync(new Model { Name = "GSX1000R" });
            await this.db.Models.AddAsync(new Model { Name = "V-Strom 650" });
            await this.db.Models.AddAsync(new Model { Name = "Africa Twin" });

            await this.db.SaveChangesAsync();
        }
    }
}

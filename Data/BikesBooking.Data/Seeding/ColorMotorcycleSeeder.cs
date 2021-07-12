namespace BikesBooking.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Models;

    public class ColorMotorcycleSeeder : ISeeder
    {
        private readonly ApplicationDbContext db;

        public ColorMotorcycleSeeder(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (this.db.Colors.Any())
            {
                return;
            }

            await this.db.Colors.AddAsync(new Color { Name = "Black" });
            await this.db.Colors.AddAsync(new Color { Name = "Red" });
            await this.db.Colors.AddAsync(new Color { Name = "Blue" });
            await this.db.Colors.AddAsync(new Color { Name = "Yellow" });
            await this.db.Colors.AddAsync(new Color { Name = "Green" });
            await this.db.Colors.AddAsync(new Color { Name = "Silver" });
            await this.db.Colors.AddAsync(new Color { Name = "White" });

            await this.db.SaveChangesAsync();
        }
    }
}

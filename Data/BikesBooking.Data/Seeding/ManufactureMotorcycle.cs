namespace BikesBooking.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Models;

    public class ManufactureMotorcycle : ISeeder
    {
        private readonly ApplicationDbContext db;

        public ManufactureMotorcycle(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (this.db.Manufacturers.Any())
            {
                return;
            }

            await this.db.Manufacturers.AddAsync(new Manufacturer { Name = "Honda", Year = 2019 });
            await this.db.Manufacturers.AddAsync(new Manufacturer { Name = "Yamaha", Year = 2020 });
            await this.db.Manufacturers.AddAsync(new Manufacturer { Name = "Kawasaki", Year = 2018 });
            await this.db.Manufacturers.AddAsync(new Manufacturer { Name = "Suzuki", Year = 2017 });
            await this.db.Manufacturers.AddAsync(new Manufacturer { Name = "Ducati", Year = 2017 });
            await this.db.Manufacturers.AddAsync(new Manufacturer { Name = "Honda", Year = 2019 });

            await this.db.SaveChangesAsync();
        }
    }
}

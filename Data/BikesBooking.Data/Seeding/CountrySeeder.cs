namespace BikesBooking.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Models;
    using BikesBooking.Data.Seeding.Models;
    using Newtonsoft.Json;

    public class CountrySeeder : ISeeder
    {
        private const string JSONADDRESS = @"..\..\Data\BikesBooking.Data\Seeding\Resources\countries.json";

        private readonly ApplicationDbContext db;

        public CountrySeeder(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (this.db.Countries.Any())
            {
                return;
            }

            var properties = JsonConvert.DeserializeObject<IEnumerable<CountryDtoInput>>(
                File.ReadAllText(JSONADDRESS));
            foreach (var jsonProp in properties)
            {
                await this.db.Countries.AddAsync(new Country { Name = jsonProp.Name, Abbreviation = jsonProp.Code });
            }

            await this.db.SaveChangesAsync();
        }
    }
}

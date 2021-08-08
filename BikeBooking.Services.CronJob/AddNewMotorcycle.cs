namespace BikeBooking.Services.CronJob
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;

    using Microsoft.EntityFrameworkCore;

    public class AddNewMotorcycle
    {
        private readonly IDeletableEntityRepository<Motorcycle> motorcycleRepository;

        public AddNewMotorcycle(IDeletableEntityRepository<Motorcycle> motorcycleRepository)
        {
            this.motorcycleRepository = motorcycleRepository;
        }

        public async Task Work()
        {
            var allMotorcycles = await this.motorcycleRepository
                .All()
                .ToListAsync();

            var latestMotocycle =
                 allMotorcycles
                 .Where(x => x.CreatedOn.Subtract(DateTime.UtcNow).Days < 5)
                 .ToList();
        }
    }
}

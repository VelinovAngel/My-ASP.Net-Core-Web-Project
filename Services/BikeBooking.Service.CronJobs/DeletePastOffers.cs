namespace BikeBooking.Service.CronJobs
{
    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class DeletePastOffers
    {
        private readonly IDeletableEntityRepository<Offer> offersRepository;

        public DeletePastOffers(IDeletableEntityRepository<Offer> offersRepository)
        {
            this.offersRepository = offersRepository;
        }

        public async Task Work()
        {
            var alloffers = await this.offersRepository
                .All()
                .ToListAsync();

            var expiredOffers =
                 alloffers
                 .Where(x => 
                 x.DropOffDate.Subtract(DateTime.UtcNow).Days == 0)
                 .ToList();

            foreach (var offer in expiredOffers)
            {
                this.offersRepository.Delete(offer);
            }

            await this.offersRepository.SaveChangesAsync();
        }
    }
}

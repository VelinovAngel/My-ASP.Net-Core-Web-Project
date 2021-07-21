namespace BikesBooking.Services.Data
{
    using System.Linq;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;

    public class DealersService : IDealersService
    {
        private readonly IRepository<PublicDealer> publicDelaer;

        public DealersService(IRepository<PublicDealer> publicDelaer)
        {
            this.publicDelaer = publicDelaer;
        }

        public bool IsAlreadyPublicDealerExist(string id)
            => this.publicDelaer.AllAsNoTracking().Any(x => x.PublicDealerId == id);
    }
}

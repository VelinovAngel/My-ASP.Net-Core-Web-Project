namespace BikesBooking.Services.Data
{
    using System.Linq;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;

    public class UserService : IUserService
    {
        private readonly IRepository<Dealer> dealer;

        public UserService(IRepository<Dealer> dealer)
        {
            this.dealer = dealer;
        }

        public int GetTotalDeales()
            => this.dealer.AllAsNoTracking().Count();
    }
}

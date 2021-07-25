namespace BikesBooking.Services.Data
{
    using System.Linq;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;

    public class UserService : IUserService
    {
        private readonly IRepository<Dealer> dealer;
        private readonly IRepository<ApplicationUser> user;

        public UserService(IRepository<Dealer> dealer,
            IRepository<ApplicationUser> user)
        {
            this.dealer = dealer;
            this.user = user;
        }

        public int GetTotalDeales()
            => this.dealer.AllAsNoTracking().Count();

        public int GetTotalUsers()
            => this.user.AllAsNoTracking().Count();
    }
}

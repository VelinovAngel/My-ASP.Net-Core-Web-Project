namespace BikesBooking.Services.Data.User
{
    using System.Linq;

    using BikesBooking.Common;
    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;

    public class UserService : IUserService
    {
        private readonly IRepository<Dealer> dealer;
        private readonly IRepository<ApplicationUser> user;
        private readonly IRepository<Client> client;

        public UserService(
            IRepository<Dealer> dealer,
            IRepository<ApplicationUser> user,
            IRepository<Client> client)
        {
            this.dealer = dealer;
            this.user = user;
            this.client = client;
        }

        public int GetTotalDeales()
            => this.dealer.AllAsNoTracking().Count();

        public int GetTotalUsers()
            => this.user.All()
            .Where(x => x.Email != GlobalConstants.AdministratorEmailAddress).Count();

        public int GetTotalClients()
            => this.client.AllAsNoTracking().Count();
    }
}

namespace BikesBooking.Services.Data
{
    using System.Linq;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;

    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> user;

        public UserService(IRepository<ApplicationUser> user)
        {
            this.user = user;
        }

        public int GetTotalUsers()
            => this.user.AllAsNoTracking().Count();
    }
}

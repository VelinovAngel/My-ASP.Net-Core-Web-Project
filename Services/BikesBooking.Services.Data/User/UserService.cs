namespace BikesBooking.Services.Data.User
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UserService : IUserService
    {
        private readonly IRepository<Dealer> dealer;
        private readonly IRepository<ApplicationUser> user;
        private UserManager<ApplicationUser> userManager;

        public UserService(
            IRepository<Dealer> dealer,
            IRepository<ApplicationUser> user,
            UserManager<ApplicationUser> userManager)
        {
            this.dealer = dealer;
            this.user = user;
            this.userManager = userManager;
        }

        public int GetTotalDeales()
            => this.dealer.AllAsNoTracking().Count();

        public int GetTotalUsers()
            => this.user.AllAsNoTracking().Count();

        public async Task<IdentityResult> AssignRole(IServiceProvider services, string email, string role)
        {
            this.userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await this.userManager.FindByEmailAsync(email);
            var result = await this.userManager.AddToRoleAsync(user, role);

            return result;
        }
    }
}

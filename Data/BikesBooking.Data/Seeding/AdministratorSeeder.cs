namespace BikesBooking.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Common;
    using BikesBooking.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class AdministratorSeeder : ISeeder
    {
        public AdministratorSeeder(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.dbContext = dbContext;
            this.serviceProvider = serviceProvider;
        }

        public static async Task<IdentityResult> AssignRole(IServiceProvider services, string email, string admin)
        {
            UserManager<ApplicationUser> userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            var result = await userManager.AddToRoleAsync(user, admin);

            return result;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any(r => r.Name == GlobalConstants.AdministratorRoleName))
            {
                await roleStore.CreateAsync(new IdentityRole(GlobalConstants.AdministratorRoleName));
            }

            var user = new ApplicationUser
            {
                FirstName = GlobalConstants.AdministratoFirtsName,
                LastName = GlobalConstants.AdministratoLastName,
                Email = GlobalConstants.AdministratorEmailAddress,
                NormalizedEmail = GlobalConstants.AdministratorNormalizedEmail,
                UserName = GlobalConstants.AdministratorEmailAddress,
                NormalizedUserName = GlobalConstants.AdministratorNormalizedEmail,
                PhoneNumber = null,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, GlobalConstants.AdministratoPassword);
                user.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(context);
                var result = userStore.CreateAsync(user);
            }

            await AssignRole(serviceProvider, user.Email, GlobalConstants.AdministratorRoleName);

            await context.SaveChangesAsync();
        }
    }
}

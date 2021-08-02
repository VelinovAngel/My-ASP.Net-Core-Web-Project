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
        private readonly ApplicationDbContext dbContext;
        private readonly IServiceProvider serviceProvider;

        public AdministratorSeeder(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.dbContext = dbContext;
            this.serviceProvider = serviceProvider;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            var admin = "Administrator";

            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any(r => r.Name == admin))
            {
                await roleStore.CreateAsync(new IdentityRole(admin));
            }

            var user = new ApplicationUser
            {
                FirstName = "Angel",
                LastName = "Velinov",
                Email = "admin@bikesbooking.com",
                NormalizedEmail = "ADMIN@BIKESBOOKING.COM",
                UserName = "admin@bikesbooking.com",
                NormalizedUserName = "ADMIN@BIKESBOOKING.COM",
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
                var hashed = password.HashPassword(user, "Velinov!");
                user.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(context);
                var result = userStore.CreateAsync(user);
            }

            await AssignRole(serviceProvider, user.Email, admin);

            await context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRole(IServiceProvider services, string email, string admin)
        {
            UserManager<ApplicationUser> userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            var result = await userManager.AddToRoleAsync(user, admin);

            return result;
        }
    }
}

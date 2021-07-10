namespace BikerBooking.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics.CodeAnalysis;

    public class BikerDbContext : IdentityDbContext
    {

        public BikerDbContext([NotNullAttribute] DbContextOptions<BikerDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

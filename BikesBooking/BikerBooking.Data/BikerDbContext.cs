namespace BikerBooking.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics.CodeAnalysis;

    public class BikerDbContext : DbContext
    {

        protected BikerDbContext()
        {
        }
        public BikerDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

    }
}

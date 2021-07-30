using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BikesBooking.Web.Areas.Identity.IdentityHostingStartup))]

namespace BikesBooking.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services)
                =>
            {
            });
        }
    }
}

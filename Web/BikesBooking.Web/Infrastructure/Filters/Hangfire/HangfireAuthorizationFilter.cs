namespace BikesBooking.Web.Infrastructure.Filters.Hangfire
{
    using BikesBooking.Common;
    using global::Hangfire.Dashboard;


    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var isInRole = httpContext.User.IsInRole(GlobalConstants.AdministratorRoleName);
            return isInRole;
        }
    }
}

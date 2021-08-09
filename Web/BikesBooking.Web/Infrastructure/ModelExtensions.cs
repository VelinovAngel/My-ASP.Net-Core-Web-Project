namespace BikesBooking.Web.Infrastructure
{
    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public static class ModelExtensions
    {
        public static string ToFriendlyUrl(this MotorcycleDetailsModel model)
            => model.Manufacturer + "-" + model.Model + "-" + model.Year;
    }

}

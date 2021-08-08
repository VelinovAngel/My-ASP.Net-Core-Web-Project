namespace BikesBooking.Web.ViewModels.Motor
{
    using System.Collections.Generic;
    using BikesBooking.Common;
    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public class AllMotorcyclesQueryModel
    {
        public const int MotorcyclesPerPage = GlobalConstants.MaxPageElements;

        public int CurrentPage { get; set; } = GlobalConstants.CurrentPage;

        public int TotalMotorcycle { get; set; }

        public IEnumerable<MotorcycleDetailsModel> Motors { get; set; }
    }
}

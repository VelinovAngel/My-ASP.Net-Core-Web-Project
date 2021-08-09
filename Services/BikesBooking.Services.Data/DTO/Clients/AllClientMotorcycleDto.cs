namespace BikesBooking.Services.Data.DTO.Clients
{
    using System.Collections.Generic;

    using BikesBooking.Common;
    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public class AllClientMotorcycleDto
    {
        public const int MotorcyclesPerPage = GlobalConstants.MaxPageFreeMotors;

        public int CurrentPage { get; set; } = GlobalConstants.CurrentPage;

        public int TotalMotorcycle { get; set; }

        public IEnumerable<MotorcycleDetailsModel> Motors { get; set; }
    }
}

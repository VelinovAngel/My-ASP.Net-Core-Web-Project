namespace BikesBooking.Services.Data.DTO.MotorcycleModels
{
    using System.Collections.Generic;

    using BikesBooking.Common;

    public class AllMotorcylesQueryDto
    {
        public const int MotorcyclesPerPage = GlobalConstants.MaxPageElements;

        public int CurrentPage { get; set; } = 1;

        public int TotalMotorcycle { get; set; }

        public IEnumerable<MotorcycleDetailsModel> Motors { get; set; }
    }
}

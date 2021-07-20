namespace BikesBooking.Services.Data.DTO.MotorcycleModels
{
    using System.Collections.Generic;

    using BikesBooking.Services.Data.DTO.Motorcycle;

    public class AllMotorcylesQueryDto
    {
        public const int MotorcyclesPerPage = 4;

        public int CurrentPage { get; set; } = 1;

        public int TotalMotorcycle { get; set; }

        public IEnumerable<MotorcycleServiceModel> Motors { get; set; }
    }
}

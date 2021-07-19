namespace BikesBooking.Services.Data.DTO.Motorcycle
{
    using System.Collections.Generic;

    public class MotorcycleQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int MotorcyclesPerPage { get; set; }

        public int TotalMotorcycles { get; set; }

        public IEnumerable<MotorcycleServiceModel> Motorcycle { get; set; }
    }
}

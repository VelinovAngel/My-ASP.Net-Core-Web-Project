namespace BikesBooking.Services.Data.DTO.MotorcycleModels
{
    using System.Collections.Generic;

    public class MotorcycleQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int MotorcyclesPerPage { get; set; }

        public int TotalMotorcycles { get; set; }

        public IEnumerable<MotorcycleDetailsModel> Motorcycle { get; set; }
    }
}

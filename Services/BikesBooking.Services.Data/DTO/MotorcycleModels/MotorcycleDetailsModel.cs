namespace BikesBooking.Services.Data.DTO.MotorcycleModels
{
    using System;

    public class MotorcycleDetailsModel : MotorcycleServiceDto
    {
        public int DealerId { get; set; }

        public string DealerName { get; set; }

        public DateTime AddedOn { get; set; }
    }
}

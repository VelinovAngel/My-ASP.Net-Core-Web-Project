namespace BikesBooking.Services.Data.DTO.MotorcycleModels
{
    using System;

    public class MotorcycleDetailsModel : MotorcycleServiceDto
    {
        public string DealerId { get; set; }

        public string Dealer { get; set; }

        public DateTime AddedOn { get; set; }
    }
}

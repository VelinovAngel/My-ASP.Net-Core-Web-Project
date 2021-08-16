namespace BikesBooking.Services.Data.DTO.Dealers
{
    using System;

    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public class AllReviewOuputDto
    {
        public string Name { get; set; }

        public string Desription { get; set; }

        public DateTime DateRelease { get; set; }

        public int Vote { get; set; }

        public MotorcycleServiceDto MotorcycleInfo { get; set; }
    }
}

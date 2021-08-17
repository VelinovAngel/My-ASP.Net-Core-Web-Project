namespace BikesBooking.Services.Data.DTO.MotorcycleModels
{
    using System;

    public class OfferSigleMotorcycleDto
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int CubicCentimetre { get; set; }

        public decimal Price { get; set; }

        public bool Available { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Url { get; set; }

        public string Type { get; set; }

        public string DealerId { get; set; }

        public DateTime BeginBooking { get; set; }

        public DateTime FinalBooking { get; set; }
    }
}

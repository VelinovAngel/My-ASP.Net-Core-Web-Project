namespace BikesBooking.Services.Data.DTO.MotorcycleModels
{
    public class OfferSigleMotorcycleDto
    {
        public int ModelId { get; set; }

        public string Manufacturer{ get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int CubicCentimetre { get; set; }

        public decimal Price { get; set; }

        public bool Available { get; set; }

        public string Url { get; set; }

        public string Type { get; set; }

        public string DealerId { get; set; }
    }
}

namespace BikesBooking.Services.Data.DTO
{
    using BikesBooking.Web.ViewModels.Motor;

    public class MotorcycleDtoOutput
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public int Year { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int CubicCentimetre { get; set; }

        public decimal Price { get; set; }

        public bool Available { get; set; }

        public string Url { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public MotorType Type { get; set; }

        public string Description { get; set; }
    }
}

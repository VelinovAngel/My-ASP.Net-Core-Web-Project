namespace BikesBooking.Services.Data.DTO.Motorcycle
{
    using System;

    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public class MotorcycleServiceModel
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

        public DateTime AddedOn { get; set; }

        public string Country { get; set; }

        public TypeOfMotors Type { get; set; }

        public string Description { get; set; }
    }
}

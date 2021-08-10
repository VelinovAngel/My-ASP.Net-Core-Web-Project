namespace BikesBooking.Services.Data.DTO.Clients
{
    using System;

    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public class AllBookedMotorcycleDto
    {
        public int Id { get; set; }

        public string Dealer { get; set; }

        public string DealerEmail { get; set; }

        public string Manufacturer { get; set; }

        public double Rating { get; set; }

        public int Year { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int CubicCentimetre { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice => this.Price * this.BookingDays.Days;

        public string Url { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public TypeOfMotors Type { get; set; }

        public string Description { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime DropOffDate { get; set; }

        public TimeSpan BookingDays => this.DropOffDate - this.PickUpDate;
    }
}

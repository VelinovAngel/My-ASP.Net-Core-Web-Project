namespace BikesBooking.Services.Data.Motorcycle
{
    using System;

    public class SearchMotorcycleInputModel
    {
        public int CityId { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime DropOffDate { get; set; }

        public int ManufacturerId { get; set; }

        public BikesBooking.Data.Models.Enum.Type Type { get; set; }
    }
}

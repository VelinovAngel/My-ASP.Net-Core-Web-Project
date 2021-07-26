namespace BikesBooking.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BikesBooking.Data.Common.Models;

    public class Offer : BaseDeletableModel<int>
    {
        public Offer()
        {
            this.Motorcycles = new HashSet<Motorcycle>();
        }

        public int Quantity { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime DropOffDate { get; set; }

        public int? BookingId { get; set; }

        public Booking Booking { get; set; }

        public virtual ICollection<Motorcycle> Motorcycles { get; set; }
    }
}

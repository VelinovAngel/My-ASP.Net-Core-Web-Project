namespace BikesBooking.Data.Models
{
    using System;

    using BikesBooking.Data.Common.Models;

    public class Booking : BaseDeletableModel<int>
    {
        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        public int Quantity { get; set; }

        public virtual Offer Offer { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime DropOffDate { get; set; }
    }
}

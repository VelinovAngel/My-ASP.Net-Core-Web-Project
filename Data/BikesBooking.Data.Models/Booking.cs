namespace BikesBooking.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BikesBooking.Data.Common.Models;

    public class Booking : BaseDeletableModel<int>
    {
        public Booking()
        {
            this.Motorcycles = new HashSet<Motorcycle>();
        }

        public int ProviderId { get; set; }

        public virtual Provider Provider { get; set; }

        public int Quantity { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime DropOffDate { get; set; }

        public virtual ICollection<Motorcycle> Motorcycles { get; set; }
    }
}

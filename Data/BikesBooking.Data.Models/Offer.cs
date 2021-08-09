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
            this.Clients = new HashSet<Client>();
        }

        public int StatisticsBooked { get; set; }

        public bool IsFree { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime DropOffDate { get; set; }

        public virtual ICollection<Client> Clients { get; set; }

        public virtual ICollection<Motorcycle> Motorcycles { get; set; }
    }
}

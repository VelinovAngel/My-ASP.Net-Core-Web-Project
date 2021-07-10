namespace BikesBooking.Data.Models
{
    using System.Collections.Generic;

    using BikesBooking.Data.Common.Models;

    public class Provider : BaseDeletableModel<int>
    {
        public Provider()
        {
            this.PublicDealers = new HashSet<PublicDealer>();
            this.PriverDealers = new HashSet<PrivateDealer>();
            this.Bookings = new HashSet<Booking>();
        }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public virtual ICollection<PublicDealer> PublicDealers { get; set; }

        public virtual ICollection<PrivateDealer> PriverDealers { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}

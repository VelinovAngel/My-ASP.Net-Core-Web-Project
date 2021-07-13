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
        }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<PublicDealer> PublicDealers { get; set; }

        public virtual ICollection<PrivateDealer> PriverDealers { get; set; }
    }
}

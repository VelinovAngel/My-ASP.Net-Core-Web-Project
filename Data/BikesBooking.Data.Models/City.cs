namespace BikesBooking.Data.Models
{
    using System.Collections.Generic;

    using BikesBooking.Data.Common.Models;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            this.Motorcycles = new HashSet<Motorcycle>();
        }

        public string Name { get; set; }

        public int Postcode { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public virtual ICollection<Motorcycle> Motorcycles { get; set; }
    }
}

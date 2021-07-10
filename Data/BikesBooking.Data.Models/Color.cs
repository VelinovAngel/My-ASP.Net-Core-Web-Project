namespace BikesBooking.Data.Models
{
    using System.Collections.Generic;

    using BikesBooking.Data.Common.Models;

    public class Color : BaseDeletableModel<int>
    {
        public Color()
        {
            this.Motorcycles = new HashSet<Motorcycle>();
        }

        public string Name { get; set; }

        public virtual ICollection<Motorcycle> Motorcycles { get; set; }
    }
}

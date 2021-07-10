namespace BikesBooking.Data.Models
{
    using System.Collections.Generic;

    using BikesBooking.Data.Common.Models;

    public class Model : BaseDeletableModel<int>
    {
        public Model()
        {
            this.Motorcycles = new HashSet<Motorcycle>();
        }

        public string Name { get; set; }

        public virtual ICollection<Motorcycle> Motorcycles { get; set; }
    }
}
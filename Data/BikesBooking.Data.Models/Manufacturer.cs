namespace BikesBooking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Common;
    using BikesBooking.Data.Common.Models;

    public class Manufacturer : BaseDeletableModel<int>
    {
        public Manufacturer()
        {
            this.Motorcycles = new HashSet<Motorcycle>();
        }

        [Required]
        [MaxLength(GlobalConstants.ManufacturerNameLength)]
        public string Name { get; set; }

        public int Year { get; set; }

        public virtual ICollection<Motorcycle> Motorcycles { get; set; }
    }
}

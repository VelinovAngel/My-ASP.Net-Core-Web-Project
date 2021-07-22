namespace BikesBooking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Common;
    using BikesBooking.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        [Required]
        [MaxLength(GlobalConstants.CountryNameLength)]
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}

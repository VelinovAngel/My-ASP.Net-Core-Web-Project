namespace BikesBooking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Common;
    using BikesBooking.Data.Common.Models;

    public class Dealer : BaseDeletableModel<int>
    {
        public Dealer()
        {
            this.Motorcycles = new HashSet<Motorcycle>();
        }

        [Required]
        [MaxLength(GlobalConstants.DealerNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.DealerAddressLength)]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Description { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public string DealerId { get; set; }

        public virtual ApplicationUser AddedDealer { get; set; }

        public virtual ICollection<Motorcycle> Motorcycles { get; set; }
    }
}

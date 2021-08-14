namespace BikesBooking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Data.Common.Models;
    using BikesBooking.Data.Models.Enum;

    public class Motorcycle : BaseDeletableModel<int>
    {
        public Motorcycle()
        {
            this.Votes = new HashSet<Vote>();
            this.Reviews = new HashSet<Review>();
        }

        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public int ModelId { get; set; }

        public virtual Model Model { get; set; }

        public int ColorId { get; set; }

        public bool IsApproved { get; set; }

        public virtual Color Color { get; set; }

        [Required]
        public int CubicCentimetre { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool Available { get; set; }

        [Required]
        public Type TypeMotor { get; set; }

        [Required]
        public string Url { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public int? OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        public int DealerId { get; set; }

        public virtual Dealer Dealer { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}

namespace BikesBooking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Data.Common.Models;
    using BikesBooking.Data.Models.Enum;

    public class Motorcycle : BaseDeletableModel<int>
    {
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public int ModelId { get; set; }

        public virtual Model Model { get; set; }

        public int ColorId { get; set; }

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

        public int? ReviewId { get; set; }

        public virtual Review Review { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public int? OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        public int? BookingId { get; set; }

        public virtual Booking Booking { get; set; }
    }
}

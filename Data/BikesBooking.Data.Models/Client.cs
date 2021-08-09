namespace BikesBooking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Common;
    using BikesBooking.Data.Common.Models;

    public class Client : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        public int? OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}

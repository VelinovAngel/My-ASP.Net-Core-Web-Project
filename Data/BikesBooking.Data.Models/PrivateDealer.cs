namespace BikesBooking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Common;
    using BikesBooking.Data.Common.Models;

    public class PrivateDealer : BaseDeletableModel<int>
    {
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

        public int ProviderId { get; set; }

        public virtual Provider Provider { get; set; }

        public string PriveteDealerId { get; set; }

        public virtual ApplicationUser AddedPrivateDealer { get; set; }
    }
}

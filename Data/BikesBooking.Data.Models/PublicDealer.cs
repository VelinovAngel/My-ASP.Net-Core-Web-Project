namespace BikesBooking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Data.Common.Models;

    public class PublicDealer : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        public string Description { get; set; }

        public int ProviderId { get; set; }

        public Provider Provider { get; set; }
    }
}

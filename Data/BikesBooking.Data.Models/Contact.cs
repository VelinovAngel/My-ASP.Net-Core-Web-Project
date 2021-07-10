namespace BikesBooking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Data.Common.Models;

    public class Contact : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(15)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Complaint { get; set; }
    }
}

namespace BikesBooking.Models.Contact
{
    using System.ComponentModel.DataAnnotations;


    public class ContactUsForm
    {
        [Required, MinLength(3), MaxLength(15)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

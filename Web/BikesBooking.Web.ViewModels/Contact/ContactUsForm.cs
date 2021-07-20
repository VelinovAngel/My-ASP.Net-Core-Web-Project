namespace BikesBooking.Web.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Common;

    public class ContactUsForm
    {
        [Required]
        [MinLength(GlobalConstants.ContactFormUsernameMin)]
        [MaxLength(GlobalConstants.ContactFormUsernameMax)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

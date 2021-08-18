namespace BikesBooking.Web.ViewModels.Client
{
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Common;

    public class SendEmailFormToDealer
    {
        [Required]
        [MinLength(GlobalConstants.ContactFormUsernameMin)]
        [MaxLength(GlobalConstants.ContactFormUsernameMax)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        public string DealerEmail { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

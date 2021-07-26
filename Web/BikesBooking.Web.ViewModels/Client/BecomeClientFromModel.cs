namespace BikesBooking.Web.ViewModels.Client
{
    using System.ComponentModel.DataAnnotations;

    using static BikesBooking.Common.GlobalConstants;

    public class BecomeClientFromModel
    {
        [Required]
        [StringLength(
           ClientValidation.ClientNameMaxLength,
           ErrorMessage = "The dealer name must be between {1} and {2} characters.",
           MinimumLength = ClientValidation.ClientNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            ClientValidation.CLientAddressMaxLength,
            ErrorMessage = "The dealer address must be between {1} and {2} characters.",
            MinimumLength = ClientValidation.ClientAddressMinLength)]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

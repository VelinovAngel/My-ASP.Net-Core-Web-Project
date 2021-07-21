namespace BikesBooking.Web.ViewModels.Dealers
{
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Common;

    public class BecomeDealerFormModel
    {
        [Required]
        [StringLength(
            GlobalConstants.DealerNameMaxLength,
            ErrorMessage = "The dealer name must be between {1} and {2} characters.",
            MinimumLength = GlobalConstants.DealerNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            GlobalConstants.DealerAddressMaxLength,
            ErrorMessage = "The dealer address must be between {1} and {2} characters.",
            MinimumLength = GlobalConstants.DealerAddressMinLength)]
        public string Address { get; set; }

        [Required]
        [StringLength(
            GlobalConstants.CountryNameLength,
            ErrorMessage = "The dealer country must be between {1} and {2} characters.",
            MinimumLength = GlobalConstants.CountryNameMinLength)]
        public string Country { get; set; }

        [Required]
        [StringLength(
            GlobalConstants.CityNameMaxLength,
            ErrorMessage = "The dealer City must be between {1} and {2} characters.",
            MinimumLength = GlobalConstants.CityNameMinLength)]
        public string City { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(
            GlobalConstants.DescriptionMaxLength,
            ErrorMessage = "The description must be between {1} and {2} characters.",
            MinimumLength = GlobalConstants.DescriptionMinLength)]
        public string Description { get; set; }
    }
}

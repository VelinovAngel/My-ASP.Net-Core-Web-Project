namespace BikesBooking.Web.ViewModels.Client
{
    using System.ComponentModel.DataAnnotations;

    using static BikesBooking.Common.GlobalConstants;

    public class BecomeClientFromModel
    {
        [Required]
        [StringLength(
            ClientValidation.CityNameMaxLength,
            ErrorMessage = "The client city name must be between {1} and {2} characters.",
            MinimumLength = ClientValidation.CityNameMinLength)]
        public string City { get; set; }

        [Required]
        [StringLength(
            ClientValidation.AddressMaxLength,
            ErrorMessage = "The client address must be between {1} and {2} characters.",
            MinimumLength = ClientValidation.AddressMinLength)]
        public string Address { get; set; }
    }
}

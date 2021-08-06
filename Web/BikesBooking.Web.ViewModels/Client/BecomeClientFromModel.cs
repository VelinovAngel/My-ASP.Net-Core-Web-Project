namespace BikesBooking.Web.ViewModels.Client
{
    using System.ComponentModel.DataAnnotations;

    using static BikesBooking.Common.GlobalConstants.ClientValidation;

    public class BecomeClientFromModel
    {
        //[Required]
        //[StringLength(
        //    CityNameMaxLength,
        //    ErrorMessage = "The client city name must be between {1} and {2} characters.",
        //    MinimumLength = CityNameMinLength)]
        public string City { get; set; }

        //[Required]
        //[StringLength(
        //    AddressMaxLength,
        //    ErrorMessage = "The client address must be between {1} and {2} characters.",
        //    MinimumLength = AddressMinLength)]
        public string Address { get; set; }
    }
}

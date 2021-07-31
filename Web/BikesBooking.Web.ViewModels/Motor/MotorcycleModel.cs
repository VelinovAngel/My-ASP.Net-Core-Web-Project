namespace BikesBooking.Web.ViewModels.Motor
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Common;

    public class MotorcycleModel
    {
        [Required]
        [MaxLength(GlobalConstants.ManufacturerNameLength)]
        public string Manufacturer { get; set; }

        [Required]
        [Range(GlobalConstants.MinYearMotorcycle, GlobalConstants.MaxYearMotorcycle)]
        public int Year { get; set; }

        [Required]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        [MaxLength(15)]
        public string Color { get; set; }

        [Required]
        [Display(Name = "Cubic Centimetre")]
        [Range(
            GlobalConstants.CubicCentimetreMin,
            GlobalConstants.CubicCentimetreMax,
            ErrorMessage = "The cubic centimetre must be between {1} c.c. and {2} c.c.!")]
        public int CubicCentimetre { get; set; }

        [Required]
        [Display(Name = "Price per day")]
        [Range(
            GlobalConstants.MotorcyclePriceMin,
            GlobalConstants.MotorcyclePriceMax,
            ErrorMessage = "The price must be between {1}.00 € and {2}.00 €!")]
        public decimal Price { get; set; }

        [Required]
        public bool Available { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        [StringLength(
            GlobalConstants.CityNameMaxLength,
            ErrorMessage = "The city name must be between {1} and {2} characters.",
            MinimumLength = GlobalConstants.CityNameMinLength)]
        public string City { get; set; }

        [Required]
        [StringLength(
            GlobalConstants.CountryNameLength,
            ErrorMessage = "The country name must be between {1} and {2} characters.",
            MinimumLength = GlobalConstants.CountryNameMinLength)]
        public string Country { get; set; }

        [Required]
        public MotorType Type { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MotorcycleDescription)]
        public string Description { get; set; }
    }
}

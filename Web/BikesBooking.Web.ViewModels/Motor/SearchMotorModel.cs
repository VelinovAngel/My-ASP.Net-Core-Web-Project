namespace BikesBooking.Web.ViewModels.Motor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public class SearchMotorModel
    {
        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Pick-up Date")]
        public DateTime PickUpDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Drop-off Date")]
        public DateTime DropOffDate { get; set; }

        public int CityCount { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public int ManufacturerId { get; set; }

        public AddMotorcycleDto Type { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CountriesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> ManufacturerItems { get; set; }
    }
}

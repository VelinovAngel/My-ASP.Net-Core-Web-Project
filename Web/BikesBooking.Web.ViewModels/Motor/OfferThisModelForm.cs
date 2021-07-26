namespace BikesBooking.Web.ViewModels.Motor
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public class OfferThisModelForm
    {
        public OfferSigleMotorcycleDto Motor { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Pick-Up Date")]
        public DateTime PickUpDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Drop-Off Date")]
        public DateTime DropOffDate { get; set; }
    }
}

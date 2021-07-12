namespace BikesBooking.Web.ViewModels.Motor
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SearchMotorModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Country { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(12)]
        public string City { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Pick-up Date")]
        public DateTime PickUpDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Drop-off Date")]
        public DateTime DropOffDate { get; set; }

        [Required]
        public MotorType Type { get; set; }
    }
}

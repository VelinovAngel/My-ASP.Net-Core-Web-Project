namespace BikesBooking.Web.ViewModels.Motor
{
    using System.ComponentModel.DataAnnotations;

    public class AddMotorcycleModel
    {
        [Required]
        [MaxLength(30)]
        public string Manufacturer { get; set; }

        [Required]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        [MaxLength(15)]
        public string Color { get; set; }

        [Required]
        [Display(Name = "Cubic Centimetre")]
        [Range(0, 1000, ErrorMessage = "The cubic centimetre must be between 50c.c. and 1250c.c.!")]
        public int CubicCentimetre { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "The price must be between 0.00€ and 1000.00€!")]
        public double Price { get; set; }

        [Required]
        public bool Available { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        [MaxLength(600)]
        public string Review { get; set; }
    }
}

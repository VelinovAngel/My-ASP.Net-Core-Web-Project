namespace BikesBooking.Web.ViewModels.Motor
{
    using System.ComponentModel.DataAnnotations;

    public class AddMotorcycleModel
    {
        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        [Display(Name = "Cubic Centimetre")]
        public int CubicCentimetre { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public bool Available { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Review { get; set; }
    }
}

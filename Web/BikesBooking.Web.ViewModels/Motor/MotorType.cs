namespace BikesBooking.Web.ViewModels.Motor
{
    using System.ComponentModel.DataAnnotations;

    public enum MotorType
    {
        Motorcycle = 1,
        Scooter = 2,
        [Display(Name = "Quad-Bike")]
        QuadBike = 3,
    }
}

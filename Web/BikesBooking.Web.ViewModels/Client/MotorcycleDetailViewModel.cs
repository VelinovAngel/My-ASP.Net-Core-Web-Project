namespace BikesBooking.Web.ViewModels.Client
{
    using System;

    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public class MotorcycleDetailViewModel : MotorcycleDetailsModel
    {
        public DateTime PickUpDate { get; set; }

        public DateTime DropOffDate { get; set; }
    }
}

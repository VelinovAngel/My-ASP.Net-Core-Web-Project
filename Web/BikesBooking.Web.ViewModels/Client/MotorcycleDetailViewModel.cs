namespace BikesBooking.Web.ViewModels.Client
{
    using System;

    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public class MotorcycleDetailViewModel
    {
        public int Id { get; set; }

        public MotorcycleDetailsModel Details { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime DropOffDate { get; set; }
    }
}

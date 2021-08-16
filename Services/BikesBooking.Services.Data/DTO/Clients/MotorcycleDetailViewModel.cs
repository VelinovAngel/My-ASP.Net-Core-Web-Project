namespace BikesBooking.Services.Data.DTO.Clients
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public class MotorcycleDetailViewModel : MotorcycleDetailsModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime PickUpDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DropOffDate { get; set; }
    }
}

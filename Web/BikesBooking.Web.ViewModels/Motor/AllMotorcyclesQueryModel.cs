namespace BikesBooking.Web.ViewModels.Motor
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BikesBooking.Services.Data.DTO.Motorcycle;

    public class AllMotorcyclesQueryModel
    {
        public const int MotorcyclesPerPage = 4;

        public int CurrentPage { get; set; } = 1;

        public int TotalMotorcycle { get; set; }

        public IEnumerable<MotorcycleServiceModel> Motors { get; set; }
    }
}

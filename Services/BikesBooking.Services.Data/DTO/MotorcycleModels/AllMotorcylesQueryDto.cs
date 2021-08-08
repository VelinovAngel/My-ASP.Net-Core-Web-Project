namespace BikesBooking.Services.Data.DTO.MotorcycleModels
{
    using System;
    using System.Collections.Generic;

    using BikesBooking.Common;

    public class AllMotorcylesQueryDto
    {
        public const int MotorcyclesPerPage = GlobalConstants.MaxPageElements;

        public int CurrentPage { get; set; } = 1;

        public int TotalMotorcycle { get; set; }

        public IEnumerable<MotorcycleDetailsModel> Motors { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime DropOffDate { get; set; }

        public int CityCount { get; set; }

        public int ManufacturerId { get; set; }

        public MotorcycleServiceDto Type { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CountriesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> ManufacturerItems { get; set; }
    }
}

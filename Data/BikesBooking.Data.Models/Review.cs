namespace BikesBooking.Data.Models
{
    using System;

    using BikesBooking.Data.Common.Models;
    using BikesBooking.Data.Models.Enum;

    public class Review : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Rating Rating { get; set; }

        public DateTime DateRelease { get; set; }
    }
}

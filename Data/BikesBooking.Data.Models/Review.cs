namespace BikesBooking.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Data.Common.Models;
    using BikesBooking.Data.Models.Enum;

    public class Review : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public Rating Rating { get; set; }

        public DateTime DateRelease { get; set; }
    }
}

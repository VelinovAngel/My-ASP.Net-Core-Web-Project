﻿namespace BikesBooking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Data.Common.Models;

    public class Client : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser AddedClient { get; set; }
    }
}
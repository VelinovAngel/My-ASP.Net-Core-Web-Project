namespace BikesBooking.Services.Data.DTO.ContactModels
{
    using System;

    using BikesBooking.Data.Models;
    using BikesBooking.Services.Mapping;

    public class ContactFormDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Complaint { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

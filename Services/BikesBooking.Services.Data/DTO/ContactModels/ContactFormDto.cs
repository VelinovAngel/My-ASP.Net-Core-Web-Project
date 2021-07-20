namespace BikesBooking.Services.Data.DTO.ContactModels
{
    using System;

    public class ContactFormDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

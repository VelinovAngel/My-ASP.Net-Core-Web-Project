namespace BikesBooking.Services.Data.DTO
{
    using System;

    public class ReviewOutputDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Vote { get; set; }

        public DateTime DateRelease { get; set; }
    }
}

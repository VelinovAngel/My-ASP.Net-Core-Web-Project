namespace BikesBooking.Data.Models
{
    using BikesBooking.Data.Common.Models;

    public class Contact : BaseDeletableModel<int>
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string Complaint { get; set; }
    }
}

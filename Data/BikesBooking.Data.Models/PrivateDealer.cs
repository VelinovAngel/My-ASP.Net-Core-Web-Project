namespace BikesBooking.Data.Models
{
    using BikesBooking.Data.Common.Models;

    public class PrivateDealer : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public int ProviderId { get; set; }

        public Provider Provider { get; set; }
    }
}
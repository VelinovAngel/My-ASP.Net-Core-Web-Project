namespace BikesBooking.Data.Models
{
    public class ClientsOffers
    {
        public int Id { get; set; }

        public int? OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        public int? MotorcycleId { get; set; }
    }
}

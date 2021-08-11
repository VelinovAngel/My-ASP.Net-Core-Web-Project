namespace BikesBooking.Data.Models
{
    using BikesBooking.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public int? MotorcycleId { get; set; }

        public virtual Motorcycle Motorcycle { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Value { get; set; }
    }
}

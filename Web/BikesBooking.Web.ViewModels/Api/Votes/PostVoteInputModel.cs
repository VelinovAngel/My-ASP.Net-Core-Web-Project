namespace BikesBooking.Web.ViewModels.Api.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class PostVoteInputModel
    {
        public int MotorcycleId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}

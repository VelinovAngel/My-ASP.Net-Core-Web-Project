namespace BikesBooking.Services.Data.Votes
{
    using System.Threading.Tasks;

    public interface IVoteService
    {
        Task SetVote(int motorcycleId, string userId, byte value);

        double GetAverageVote(int? motorcycleId);

        int GetVoteByUser(string userId);
    }
}

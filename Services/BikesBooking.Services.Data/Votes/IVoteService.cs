namespace BikesBooking.Services.Data.Votes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO;

    public interface IVoteService
    {
        Task SetVote(int motorcycleId, string userId, byte value);

        double GetAverageVote(int? motorcycleId);

        int GetVoteByUser(string userId);

        IEnumerable<ReviewOutputDto> GetLastestThreeFeedBack();
    }
}

namespace BikesBooking.Services.Data.Votes
{
    using System;

    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;

    public class VoteService : IVoteService
    {
        private readonly IRepository<Vote> votesRepository;

        public VoteService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVote(int? motorcyleId)
        {
            var votes = this.votesRepository.All().Where(x => x.MotorcycleId == motorcyleId);
            if (votes.Count() == 0)
            {
                return 0;
            }

            return votes.Average(x => x.Value);
        }

        public async Task SetVote(int motorcycleId, string userId, byte value)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.MotorcycleId == motorcycleId && x.UserId == userId);
            if (vote == null)
            {
                vote = new Vote
                {
                    MotorcycleId = motorcycleId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }
    }
}

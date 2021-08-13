namespace BikesBooking.Services.Data.Votes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.DTO;

    public class VoteService : IVoteService
    {
        private readonly IRepository<Vote> votesRepository;
        private readonly IRepository<Motorcycle> motorcycleRepository;
        private readonly IRepository<Review> reviewRepository;

        public VoteService(
            IRepository<Vote> votesRepository,
            IRepository<Motorcycle> motorcycleRepository,
            IRepository<Review> reviewRepository)
        {
            this.votesRepository = votesRepository;
            this.motorcycleRepository = motorcycleRepository;
            this.reviewRepository = reviewRepository;
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

        public int GetVoteByUser(string userId)
            => this.votesRepository.All().FirstOrDefault(x => x.UserId == userId).Value;

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

        public IEnumerable<ReviewOutputDto> GetLastestThreeFeedBack()
        => this.reviewRepository.All()
            .Select(x => new ReviewOutputDto
            {
                Name = x.Name,
                DateRelease = x.DateRelease,
                Description = x.Description,
                Vote = x.Vote,
            })
                .OrderByDescending(x => x.Vote)
                .ThenByDescending(c => c.DateRelease)
                .ToList().Take(3);
    }
}

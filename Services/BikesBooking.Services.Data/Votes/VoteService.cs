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

        public VoteService(
            IRepository<Vote> votesRepository,
            IRepository<Motorcycle> motorcycleRepository)
        {
            this.votesRepository = votesRepository;
            this.motorcycleRepository = motorcycleRepository;
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
        {
            var feedbacks = new List<ReviewOutputDto>();

            var allFeedbackForAllMotorcycle = this.motorcycleRepository.All()
                .Where(x => x.Reviews.Count > 0)
                .Select(x => x.Reviews.OrderByDescending(c => c.CreatedOn).Take(1).ToList())
                .ToList();
            int counter = 0;
            bool isFound = false;
            foreach (var feedbackForMotorcycle in allFeedbackForAllMotorcycle)
            {
                foreach (var item in feedbackForMotorcycle)
                {
                    var feedback = new ReviewOutputDto
                    {
                        Name = item.Name,
                        Description = item.Description,
                        DateRelease = item.DateRelease,
                        Vote = item.Vote,
                    };
                    feedbacks.Add(feedback);
                    counter++;
                    if (counter == 3)
                    {
                        isFound = true;
                        break;
                    }
                }

                if (isFound)
                {
                    break;
                }
            }

            return feedbacks;
        }
    }
}

namespace BikesBooking.Web.Controllers.ApiController
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.Votes;
    using BikesBooking.Web.Infrastructure;
    using BikesBooking.Web.ViewModels.Api.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/Vote")]
    public class VoteController : BaseController
    {
        private readonly IVoteService voteService;

        public VoteController(IVoteService voteService)
        {
            this.voteService = voteService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteResponseModel>> Post(PostVoteInputModel input)
        {
            var userId = this.User.GetId();
            await this.voteService.SetVote(input.MotorcycleId, userId, input.Value);
            var averageVote = this.voteService.GetAverageVote(input.MotorcycleId);
            return new PostVoteResponseModel { AvarageVote = averageVote };
        }
    }
}

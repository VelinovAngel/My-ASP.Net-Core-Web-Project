namespace BikesBooking.Web.Api
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [ApiController]
    public class CityApiController : ControllerBase
    {
        private readonly IHomeService homeService;

        public CityApiController(IHomeService homeService)
        {
            this.homeService = homeService;
        }


        [Route("api/GetCity")]
        [HttpGet]
        public async Task<SelectList> GetCityAsync(int id)
        {
            var model = await this.homeService.GetAllCitiesByCountryIdAsync(id);
            return new SelectList(model, "Id", "Name");
        }
    }
}

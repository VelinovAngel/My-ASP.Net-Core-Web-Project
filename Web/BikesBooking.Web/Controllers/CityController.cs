namespace BikesBooking.Web.Controllers
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CityController : BaseController
    {
        private readonly IHomeService homeService;

        public CityController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        [ActionName("GetCity")]
        public async Task<IActionResult> GetCityAsync(int id)
        {
            var model = await this.homeService.GetAllCitiesByCountryIdAsync(id);

            return this.Json(new SelectList(model, "Id", "Name"));
        }
    }
}

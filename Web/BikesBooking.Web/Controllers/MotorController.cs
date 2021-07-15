namespace BikesBooking.Web.Controllers
{
    using BikesBooking.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class MotorController : Controller
    {
        private readonly IGetAllModelsService getAllModels;

        public MotorController(IGetAllModelsService getAllModels)
        {
            this.getAllModels = getAllModels;
        }
    }
}

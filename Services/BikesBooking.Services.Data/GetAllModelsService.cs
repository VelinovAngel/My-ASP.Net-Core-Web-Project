namespace BikesBooking.Services.Data
{
    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;

    public class GetAllModelsService
    {
        private readonly IRepository<Model> modelsRepository;

        public GetAllModelsService(IRepository<Model> modelsRepository)
        {
            this.modelsRepository = modelsRepository;
        }
    }
}

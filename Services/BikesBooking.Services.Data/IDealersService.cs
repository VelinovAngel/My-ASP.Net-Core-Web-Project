namespace BikesBooking.Services.Data
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.Dealers;

    public interface IDealersService
    {
        bool IsAlreadyDealerExist(string id);

        Task CreateDealerAsync(CreateDealerDto dealer, string userId);

        CurrDealerIdDto GetCurrentDealerId(CurrDealerIdDto dealerId, string userId);

        public int GetDealerId(string userId);

        string GetCurrentDealerEmail(int id);
    }
}

namespace BikesBooking.Services.Data.Dealer
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.Dealers;

    public interface IDealersService
    {
        Task CreateDealerAsync(CreateDealerDto dealer, string userId);

        public int GetDealerId(string userId);

        string GetCurrentDealerEmail(int id);

        public bool IsDealer(string userId);
    }
}

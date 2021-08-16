namespace BikesBooking.Services.Data.Dealer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.Dealers;

    public interface IDealersService
    {
        Task CreateDealerAsync(CreateDealerDto dealer, string userId, string imgUrl);

        int GetDealerId(string userId);

        string GetDealerIdByUser(string userId);

        string GetCurrentDealerEmail(int id);

        bool IsDealer(string userId);

        CreateDealerDto GetCurrentDealerInfo(int id);

        Task<bool> Edit(CreateDealerDto dealer, int id, string imgUrl);

        IEnumerable<AllReviewOuputDto> ReadAllReviewFromCliet(int motorcycleId);
    }
}

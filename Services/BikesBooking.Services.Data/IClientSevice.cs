namespace BikesBooking.Services.Data
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.Clients;

    public interface IClientSevice
    {
        Task CreateDealerAsync(CreateClientDto client, string userId);

        bool IsAlreadyClientExist(string id);

        string GetCurrentClientEmail(int id);

        public int GetClientId(string userId);
    }
}

namespace BikesBooking.Services.Data.Client
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.Clients;

    public interface IClientService
    {
        Task CreateClientAsync(CreateClientDto client, string userId);

        bool IsAlreadyClientExist(string id);

        string GetCurrentClientEmail(int id);

        public int GetClientId(string userId);
    }
}

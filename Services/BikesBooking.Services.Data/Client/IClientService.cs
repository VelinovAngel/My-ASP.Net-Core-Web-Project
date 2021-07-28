namespace BikesBooking.Services.Data.Client
{
    using System.Threading.Tasks;

    public interface IClientService
    {
        Task CreateClientAsync(string userId, string address, string city);

        bool IsAlreadyClientExist(string id);

        string GetCurrentClientEmail(int id);

        public int GetClientId(string userId);
    }
}

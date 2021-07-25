namespace BikesBooking.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.Clients;

    public class ClientService : IClientSevice
    {
        public Task CreateDealerAsync(CreateClientDto client, string userId)
        {
            throw new NotImplementedException();
        }

        public int GetClientId(string userId)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentClientEmail(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsAlreadyClientExist(string id)
        {
            throw new NotImplementedException();
        }
    }
}

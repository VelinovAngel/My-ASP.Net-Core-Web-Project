namespace BikesBooking.Services.Data.Client
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.DTO.Clients;

    public class ClientService : IClientService
    {
        private readonly IRepository<Client> clientRepository;

        public ClientService(IRepository<Client> clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public async Task CreateClientAsync(CreateClientDto client, string userId)
        {
            var currClient = new Client
            {
                Name = client.Name,
                Email = client.Email,
                Address = client.Address,
                ClientId = userId,
            };

            await this.clientRepository.AddAsync(currClient);
            await this.clientRepository.SaveChangesAsync();
        }

        public int GetClientId(string userId)
             => this.clientRepository.AllAsNoTracking()
            .Where(x => x.ClientId == userId)
            .Select(d => new { Id = d.Id })
            .FirstOrDefault().Id;

        public string GetCurrentClientEmail(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsAlreadyClientExist(string id)
            => this.clientRepository.AllAsNoTracking().Any(x => x.ClientId == id);
    }
}

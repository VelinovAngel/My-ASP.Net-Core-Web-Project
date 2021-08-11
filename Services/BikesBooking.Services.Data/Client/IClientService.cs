namespace BikesBooking.Services.Data.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.Clients;
    using BikesBooking.Services.Data.DTO.MotorcycleModels;

    public interface IClientService
    {
        Task CreateClientAsync(string userId, string address, string city);

        IEnumerable<BookedMotorcycleDto> GetAllListOfMotorcycleByClietId(int clientId);

        BookedMotorcycleDto GetSingleBookedMotorcycleByClientId(int clientId, int motorcycleId);

        bool IsAlreadyClientExist(string userId);

        string GetCurrentClientEmail(string userId);

        public int GetClientId(string userId);

        Task<bool> BookedMotorcycleByClient(int clientId, int offerId, DateTime pickUp, DateTime dropOff, int motorcycleId);

        int GetCurrentOfferId(DateTime pickUp, DateTime dropOff);

        ClientServiceModel GetCurrentClient(string userId);
    }
}

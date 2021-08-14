namespace BikesBooking.Services.Data.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.Clients;

    public interface IClientService
    {
        Task CreateClientAsync(string userId, string address, string city);

        Task<bool> Edit(int clientId, string address, string city);

        IEnumerable<BookedMotorcycleDto> GetAllListOfMotorcycleByClietId(int clientId);

        BookedMotorcycleDto GetSingleBookedMotorcycleByClientId(int clientId, int motorcycleId);

        bool IsAlreadyClientExist(string userId);

        string GetCurrentClientEmail(string userId);

        int GetClientId(string userId);

        string GetClientIdByUser(string userId);

        string GetCurrentClientInfo(int id);

        Task<bool> BookThisMotorcycleByClient(int clientId, int offerId, DateTime pickUp, DateTime dropOff, int motorcycleId);

        int GetCurrentOfferId(DateTime pickUp, DateTime dropOff);

        ClientServiceModel GetCurrentClient(string userId);

        Task CreaterReviewByUser(int motorcycleId, byte value, string username, string description);
    }
}

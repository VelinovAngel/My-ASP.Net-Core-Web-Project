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
        private readonly IRepository<Offer> offerRepository;
        private readonly IRepository<ApplicationUser> userRepository;

        public ClientService(
            IRepository<Client> clientRepository,
            IRepository<Offer> offerRepository,
            IRepository<ApplicationUser> userRepository)
        {
            this.clientRepository = clientRepository;
            this.offerRepository = offerRepository;
            this.userRepository = userRepository;
        }

        public bool BookedMotorcycleByClient(int clientId, int offerId, DateTime pickUp, DateTime dropOff)
        {
            var currOffer = this.offerRepository.AllAsNoTracking()
                .Where(x => x.Id == offerId)
                .FirstOrDefault();
            if (currOffer == null)
            {
                return false;
            }

            if (pickUp < currOffer.PickUpDate && dropOff > currOffer.DropOffDate)
            {
                return false;
            }

            var currClient = this.clientRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.Id == clientId);

            if (currOffer.DropOffDate >= dropOff)
            {
                currOffer.PickUpDate = dropOff.AddDays(1);
                if (currOffer.PickUpDate > currOffer.DropOffDate)
                {
                    currOffer.PickUpDate = currOffer.DropOffDate;
                }
            }

            currOffer.IsFree = true;
            currOffer.StatisticsBooked++;

            currClient.OfferId = currOffer.Id;
            this.clientRepository.Update(currClient);
            this.clientRepository.SaveChangesAsync();
            this.offerRepository.Update(currOffer);
            this.offerRepository.SaveChangesAsync();
            return true;
        }

        public bool BookedMotorcycleByClient(int id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateClientAsync(string userId, string address, string city)
        {
            var firstName = this.userRepository.AllAsNoTracking()
                               .Where(x => x.Id == userId)
                               .FirstOrDefault().FirstName;
            var lastName = this.userRepository.AllAsNoTracking()
                               .Where(x => x.Id == userId)
                               .FirstOrDefault().LastName;
            var fullName = $"{firstName} {lastName}";
            var email = this.userRepository.AllAsNoTracking().Where(x => x.Id == userId).FirstOrDefault().Email;
            var completeAddress = $"{city} - {address}";
            var currClient = new Client
            {
                Name = fullName,
                Email = email,
                Address = completeAddress,
                UserId = userId,
            };

            await this.clientRepository.AddAsync(currClient);
            await this.clientRepository.SaveChangesAsync();
        }

        public int GetClientId(string userId)
             => this.clientRepository.AllAsNoTracking()
            .Where(x => x.UserId == userId)
            .Select(d => new { Id = d.Id })
            .FirstOrDefault().Id;

        public ClientServiceModel GetCurrentClient(string userId)
            => this.clientRepository.AllAsNoTracking()
            .Where(x => x.UserId == userId)
            .Select(x => new ClientServiceModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Email = x.Email,
                UserId = userId,
            }).FirstOrDefault();

        public string GetCurrentClientEmail(string userId)
            => this.clientRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == userId).Email;

        public int GetCurrentOfferId(DateTime pickUp, DateTime dropOff)
            => this.offerRepository
                   .AllAsNoTracking()
                   .Where(x => x.PickUpDate == pickUp &&
                               x.DropOffDate == dropOff)
                   .FirstOrDefault().Id;

        public bool IsAlreadyClientExist(string userId)
            => this.clientRepository.AllAsNoTracking().Any(x => x.UserId == userId);
    }
}

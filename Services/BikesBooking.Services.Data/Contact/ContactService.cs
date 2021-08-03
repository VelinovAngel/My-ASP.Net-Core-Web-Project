namespace BikesBooking.Services.Data.Contact
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.DTO.ContactModels;
    using BikesBooking.Services.Data.DTO.UserModels;
    using Microsoft.EntityFrameworkCore;

    public class ContactService : IContactService
    {
        private readonly IDeletableEntityRepository<Contact> contactRepository;

        public ContactService(IDeletableEntityRepository<Contact> contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public IEnumerable<ContactFormDto> GetAllMessages()
            => this.contactRepository.AllAsNoTracking()
            .AsQueryable()
            .Select(x => new ContactFormDto
            {
                Id = x.Id,
                Username = x.Username,
                Email = x.Email,
                Complaint = x.Complaint,
                Description = x.Description,
                CreatedOn = x.CreatedOn,
            })
            .ToList();

        public ContactFormDto GetSingleMessage(int id)
            => this.GetAllMessages().Where(x => x.Id == id).FirstOrDefault();

        public async Task SendContactMessageAsync(ContactFormDto contact)
        {
            var message = new Contact
            {
                Username = contact.Username,
                Email = contact.Email,
                Complaint = contact.Complaint,
                Description = contact.Description,
            };

            await this.contactRepository.AddAsync(message);
            await this.contactRepository.SaveChangesAsync();
        }

        public UserInfoDto GetInfoFromUser(int id)
            => this.contactRepository.AllAsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new UserInfoDto
            {
                Id = x.Id,
                Username = x.Username,
                Email = x.Email,
            })
            .FirstOrDefault();
    }
}

namespace BikesBooking.Services.Data
{
    using System.Threading.Tasks;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;
    using BikesBooking.Services.Data.DTO.ContactModels;

    public class ContactService : IContactService
    {
        private readonly IDeletableEntityRepository<Contact> contactRepository;

        public ContactService(IDeletableEntityRepository<Contact> contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task SendContactMessageAsync(ContactFormDto contact)
        {
            var message = new Contact
            {
                Username = contact.Name,
                Email = contact.Email,
                Complaint = contact.Subject,
                Description = contact.Description,
            };

            await this.contactRepository.AddAsync(message);
            await this.contactRepository.SaveChangesAsync();
        }
    }
}

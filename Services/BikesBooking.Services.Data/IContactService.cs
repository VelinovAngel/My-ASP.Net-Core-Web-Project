namespace BikesBooking.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.ContactModels;

    public interface IContactService
    {
        Task SendContactMessageAsync(ContactFormDto contact);

        Task<IEnumerable<ContactFormDto>> GetAllMessages();
    }
}

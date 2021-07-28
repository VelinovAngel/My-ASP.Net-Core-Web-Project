namespace BikesBooking.Services.Data.Contact
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.ContactModels;
    using BikesBooking.Services.Data.DTO.UserModels;

    public interface IContactService
    {
        Task SendContactMessageAsync(ContactFormDto contact);

        Task<IEnumerable<ContactFormDto>> GetAllMessages();

        ContactFormDto GetSingleMessage(int id);

        UserInfoDto GetInfoFromUser(int id);
    }
}

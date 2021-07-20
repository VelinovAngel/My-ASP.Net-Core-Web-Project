namespace BikesBooking.Services.Data
{
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO.ContactModels;

    public interface IContactService
    {
        Task SendContactMessageAsync(ContactFormDto contact);
    }
}

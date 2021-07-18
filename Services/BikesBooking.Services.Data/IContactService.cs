namespace BikesBooking.Services.Data
{
    using System.Threading.Tasks;

    using BikesBooking.Web.ViewModels.Contact;

    public interface IContactService
    {
        Task SendContactMessageAsync(ContactUsForm contact);
    }
}

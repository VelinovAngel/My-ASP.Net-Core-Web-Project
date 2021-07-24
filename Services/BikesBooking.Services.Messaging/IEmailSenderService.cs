namespace BikesBooking.Services.Messaging
{
    public interface IEmailSenderService
    {
       void SendMail(string from, string to, string subject, string html);
    }
}

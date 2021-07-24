namespace BikesBooking.Web.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;

    public class SendEmailForm
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Subject { get; set; }

        public string ClientName { get; set; }

        public string Email { get; set; }
    }
}

namespace BikesBooking.Services.Data.DTO.Dealers
{
    using Microsoft.AspNetCore.Http;

    public class CreateDealerDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}

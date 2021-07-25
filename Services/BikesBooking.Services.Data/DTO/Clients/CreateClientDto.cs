namespace BikesBooking.Services.Data.DTO.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateClientDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}

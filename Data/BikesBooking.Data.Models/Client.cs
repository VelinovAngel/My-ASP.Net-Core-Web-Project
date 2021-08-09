namespace BikesBooking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BikesBooking.Common;
    using BikesBooking.Data.Common.Models;

    public class Client : BaseDeletableModel<int>
    {
        public Client()
        {
            this.Offers = new HashSet<ClientsOffers>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        public virtual ICollection<ClientsOffers> Offers { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}

namespace BikesBooking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BikesBooking.Data.Common.Repositories;
    using BikesBooking.Data.Models;

    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<Country> countreisRepository;

        public HomeService(IDeletableEntityRepository<Country> countreisRepository)
        {
            this.countreisRepository = countreisRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs()
            => this.countreisRepository.All().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList()
            .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name.ToString()));
    }
}

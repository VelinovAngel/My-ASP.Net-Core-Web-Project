namespace BikesBooking.Services.Data
{
    using System.Collections.Generic;

    using BikesBooking.Services.Data.DTO;

    public interface IHomeService
    {
        IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs();

        IEnumerable<CityDtoOutput> GetAllCitiesByCountryId(int id);
    }
}

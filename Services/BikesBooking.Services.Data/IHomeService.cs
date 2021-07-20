namespace BikesBooking.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BikesBooking.Services.Data.DTO;

    public interface IHomeService
    {
        IEnumerable<KeyValuePair<string, string>> GetKeyValuePairsCoutries();

        IEnumerable<KeyValuePair<string, string>> GetKeyValuePairsModels();

        Task<IEnumerable<CityDtoOutput>> GetAllCitiesByCountryIdAsync(int id);

        int GetAllCityCount();
    }
}

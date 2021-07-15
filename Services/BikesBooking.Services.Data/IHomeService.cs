namespace BikesBooking.Services.Data
{
    using System.Collections.Generic;

    public interface IHomeService
    {
        IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs();
    }
}

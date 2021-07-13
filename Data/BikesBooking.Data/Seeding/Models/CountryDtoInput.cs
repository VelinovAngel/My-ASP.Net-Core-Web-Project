namespace BikesBooking.Data.Seeding.Models
{
    public class CountryDtoInput
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public CitiesDtoInput[] Cities { get; set; }
    }
}

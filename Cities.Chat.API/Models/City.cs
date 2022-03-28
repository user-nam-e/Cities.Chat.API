namespace Cities.Chat.API.Models
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; } = "";
        public string CountryName { get; set; } = "";
        public string FlagId { get; set; } = "";
        //public string SecondCityName { get; set; } = "";
        //public string SecondFlagId { get; set; } = "";
    }
}

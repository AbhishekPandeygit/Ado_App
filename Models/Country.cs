namespace ado_app.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int countryid { get; set; }
    }
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int stateid { get; set; }
    }
}

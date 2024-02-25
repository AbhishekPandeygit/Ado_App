using System.ComponentModel.DataAnnotations;

namespace ado_app.Models
{
    public class Customer
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string gender { get; set; }
        public int country_id { get; set; }
        public int state_id { get; set; }
        public int city_id { get; set; }

    }

    public enum Action
    {
        Success = 1, Error = 0, EmailExist = 2
    }
}

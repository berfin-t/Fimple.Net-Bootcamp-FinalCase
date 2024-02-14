using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankSystem.Domain.Entities
{
    public class LoginModel
    {
        [Key]
        [JsonIgnore]
        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Usersurname { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public DateTime LoginDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public UserModel User { get; set; }
    }
}

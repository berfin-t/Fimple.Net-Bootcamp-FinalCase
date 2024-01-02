using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankSystem.Domain.Entities
{
    public class LoginModel
    {
        [Key]
        public string LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public UserModel Users { get; set; }
    }
}

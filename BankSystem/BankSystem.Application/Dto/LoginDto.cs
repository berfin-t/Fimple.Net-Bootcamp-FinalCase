using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankSystem.Application.Dto
{
    public class LoginDto
    {
        [Key]
        [JsonIgnore]
        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Usersurname { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public DateTime LoginDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public UserDto Users { get; set; }
    }
}

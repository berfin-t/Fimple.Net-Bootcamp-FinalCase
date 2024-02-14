using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankSystem.Application.Dto
{
    public class AccountDto
    {
        [Key]
        [JsonIgnore]
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        [JsonIgnore]
        public string AccountType { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public UserDto User { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankSystem.Domain.Entities
{
    public class AccountModel
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
        public UserModel User { get; set; }
    }
}

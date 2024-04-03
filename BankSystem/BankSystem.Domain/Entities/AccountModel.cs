using BankSystem.Data.Entities;
using BankSystem.Data.Enums;
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
        public AccountType AccountType { get; set; } 
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public List<PaymentModel> Payments { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public UserModel User { get; set; }
    }
}

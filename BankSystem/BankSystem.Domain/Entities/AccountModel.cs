using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankSystem.Domain.Entities
{
    public class AccountModel
    {
        [Key]
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountType { get; set; } // e.g., Savings, Checking
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public UserModel User { get; set; }
        [JsonIgnore]
        public List<TransactionModel> Transactions { get; set; }
    }
}

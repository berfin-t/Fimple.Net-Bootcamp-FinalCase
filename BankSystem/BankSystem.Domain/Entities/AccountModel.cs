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

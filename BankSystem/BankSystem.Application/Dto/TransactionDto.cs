using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankSystem.Application.Dto
{
    public class TransactionDto
    {
        [Key]
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string TransactionType { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public AccountDto Account { get; set; }

    }
}

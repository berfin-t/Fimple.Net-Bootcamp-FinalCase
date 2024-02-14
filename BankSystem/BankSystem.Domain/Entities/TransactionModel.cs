using BankSystem.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Entities
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public TransactionType TransactionType { get; set; }
        public int AccountId { get; set; }

        //Navigation Properties
        public AccountModel Account { get; set; }

    }
}

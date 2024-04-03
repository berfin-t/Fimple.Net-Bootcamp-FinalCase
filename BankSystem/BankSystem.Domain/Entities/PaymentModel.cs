using BankSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankSystem.Data.Entities
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
        public TimePeriod TimePeriod { get; set; }
        public int PaymentFrequency { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public int AccountId { get; set; }

        [JsonIgnore]
        //Navigation Properties
        public AccountModel Accounts { get; set; }
    }
}

using BankSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Data.Enums;

namespace BankSystem.Application.Dto
{
    public class LoanDto
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public decimal LoanAmount { get; set; }
        public LoanType LoanType { get; set; }
        public int NumberOfTotalPayments { get; set; }
        public decimal MonthlyPayment => RemainingDebt / LoanTerm;
        public int LoanTerm { get; set; }
        public decimal RemainingDebt { get; set; }
        [NotMapped]
        public bool IsPaid => RemainingDebt == 0;
        public int NumberOfTimelyPayments { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public DateTime NextPaymentDueDate { get; set; }
        public int UserId { get; set; }

        //Navigation Properties
        public UserDto User { get; set; }
    }
}

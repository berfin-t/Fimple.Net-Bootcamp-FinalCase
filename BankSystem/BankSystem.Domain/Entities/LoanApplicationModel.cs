using BankSystem.Data.Enums;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Entities
{
    public class LoanApplicationModel
    {
        public int Id { get; set; }
        public LoanType LoanType { get; set; }

        public decimal LoanAmount { get; set; }
        public DateTime CreatedAt { get; set; }

        public int LoanTerm { get; set; }

        public LoanApplicationStatus LoanApplicationStatus { get; set; }

        public int UserId { get; set; }

        //Navigation Properties
        public UserModel User { get; set; }
    }
}

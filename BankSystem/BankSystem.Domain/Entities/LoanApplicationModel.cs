using BankSystem.Data.Enums;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankSystem.Domain.Entities
{
    public class LoanApplicationModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public LoanType LoanType { get; set; }

        public decimal LoanAmount { get; set; }
        public DateTime CreatedAt { get; set; }

        public int LoanTerm { get; set; }
        [JsonIgnore]
        public LoanApplicationStatus LoanApplicationStatus { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public UserModel User { get; set; }
    }
}

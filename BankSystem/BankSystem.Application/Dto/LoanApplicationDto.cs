using BankSystem.Application.Dto;
using BankSystem.Data.Enums;
using BankSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankSystem.Business.Dto
{
    public class LoanApplicationDto
    {
        public int Id { get; set; }
        public LoanType LoanType { get; set; }

        public decimal LoanAmount { get; set; }

        public int LoanTerm { get; set; }

        public LoanApplicationStatus LoanApplicationStatus { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public UserDto User { get; set; }
    }
}

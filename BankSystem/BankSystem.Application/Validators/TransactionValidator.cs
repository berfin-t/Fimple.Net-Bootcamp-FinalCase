using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Application.Dto;
using BankSystem.Domain.Entities;

namespace BankSystem.Application.Validators
{
    public class TransactionValidator : AbstractValidator<TransactionModel>
    {
        public TransactionValidator() {
            RuleFor(r => r.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0.");

        }
    }
}

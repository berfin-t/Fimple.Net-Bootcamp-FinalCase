using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Entities;
using FluentValidation;

namespace BankSystem.Application.Validators
{
    public class UpdateAccountValidator : AbstractValidator<AccountModel>
    {
        public UpdateAccountValidator() {
            RuleFor(r => r.Balance).GreaterThanOrEqualTo(0).WithMessage("There is not enough balance in your account");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using BankSystem.Domain.Entities;

namespace BankSystem.Application.Validators
{
    public class CreateAccountValidator :  AbstractValidator<AccountModel>
    {
        public CreateAccountValidator() {
            RuleFor(r => r.Balance).GreaterThanOrEqualTo(100).WithMessage("Min bakiye miktarı 100 olmalıdır.");

        }
    }
}

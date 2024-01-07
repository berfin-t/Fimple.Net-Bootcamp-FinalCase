using FluentValidation;
using BankSystem.Domain.Entities;

namespace BankSystem.Application.Validators
{
    public class CreateAccountValidator :  AbstractValidator<AccountModel>
    {
        public CreateAccountValidator() {
            RuleFor(r => r.Balance).GreaterThanOrEqualTo(100).WithMessage("The minimum balance amount must be 100.");

        }
    }
}

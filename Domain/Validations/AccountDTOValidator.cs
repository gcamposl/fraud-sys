using Domain.DTOs;
using FluentValidation;

namespace Domain.Validations
{
    public class AccountDTOValidator : AbstractValidator<AccountDTO>
    {
        public AccountDTOValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe o CPF!");

            RuleFor(x => x.AccountNumber)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("O número da conta deve ser maior ou igual a 0!");

            RuleFor(x => x.Agency)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("O número da agência deve ser maior ou igual a 0!");

            RuleFor(x => x.Limit)
                .NotEmpty()
                .NotNull()
                // bizarro como isso não funciona direito... 
                .GreaterThanOrEqualTo(0)
                .WithMessage("O limite deve ser maior ou igual a 0!");
        }
    }
}
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
                .GreaterThan(0)
                .WithMessage("O número da conta deve ser maior que 0!");

            RuleFor(x => x.Agency)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("O número da agência deve ser maior que 0!");

            RuleFor(x => x.Limit)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("O limite deve ser maior que 0!");
        }
    }
}
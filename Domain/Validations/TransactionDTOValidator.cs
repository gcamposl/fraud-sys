using Domain.DTOs;
using FluentValidation;

namespace Domain.Validations
{
    public class TransactionDTOValidator : AbstractValidator<TransactionDTO>
    {
        public TransactionDTOValidator()
        {
            RuleFor(x => x.Source)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe o CPF de origem!");

            RuleFor(x => x.Destiny)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe o CPF de destino!");

            RuleFor(x => x.Value)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("O valor da transferência deve ser maior que 0!");
        }
    }
}
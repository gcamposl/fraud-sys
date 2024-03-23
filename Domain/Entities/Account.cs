using Domain.Validations;

namespace Domain.Entities
{
    public sealed class Account
    {
        public string Cpf { get; set; }
        public int AgencyNumber { get; set; }
        public int AccountNumber { get; set; }
        public decimal Limit { get; set; }

        public Account(string cpf, int agencyNumber, int accountNumber, decimal limit)
        {
            Validate(cpf, agencyNumber, accountNumber, limit);
        }

        public void Validate(string cpf, int agencyNumber, int accountNumber, decimal limit)
        {
            EntitieValidator.When(string.IsNullOrEmpty(cpf), "Informe o cpf!");
            EntitieValidator.When(agencyNumber <= 0, "O número da agência deve ser maior que 0!");
            EntitieValidator.When(accountNumber <= 0, "O número da conta deve ser maior que 0!");
            EntitieValidator.When(limit < 0, "O limite deve ser maior que 0!");

            Cpf = cpf;
            AgencyNumber = agencyNumber;
            AccountNumber = accountNumber;
            Limit = limit;
        }
    }
}
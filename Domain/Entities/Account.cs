using Domain.Validations;

namespace Domain.Entities
{
    public sealed class Account
    {
        public string Cpf { get; set; }
        public int Agency { get; set; }
        public int AccountNumber { get; set; }
        public decimal Limit { get; set; }

        public Account(string cpf, int agency, int accountNumber, decimal limit)
        {
            Validate(cpf, agency, accountNumber, limit);
        }

        public void Validate(string cpf, int agency, int accountNumber, decimal limit)
        {
            EntitieValidator.When(string.IsNullOrEmpty(cpf), "Informe o cpf!");
            EntitieValidator.When(agency <= 0, "O número da agência deve ser maior que 0!");
            EntitieValidator.When(accountNumber <= 0, "O número da conta deve ser maior que 0!");
            EntitieValidator.When(limit < 0, "O limite deve ser maior que 0!");

            Cpf = cpf;
            Agency = agency;
            AccountNumber = accountNumber;
            Limit = limit;
        }
    }
}
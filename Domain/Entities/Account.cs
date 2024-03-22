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
            Cpf = cpf;
            AgencyNumber = agencyNumber;
            AccountNumber = accountNumber;
            Limit = limit;
        }

        public void Validate(string cpf, int agencyNumber, int accountNumber, decimal limit)
        {
            EntitieValidation.When(string.IsNullOrEmpty(cpf), "Informe o cpf!");
            EntitieValidation.When(agencyNumber <= 0, "O número da agência deve ser maior que 0!");
            EntitieValidation.When(accountNumber <= 0, "O número da conta deve ser maior que 0!");
            EntitieValidation.When(limit < 0, "O limite deve ser maior que 0!");

            Cpf = cpf;
            AgencyNumber = agencyNumber;
            AccountNumber = accountNumber;
            Limit = limit;
        }
    }
}
using Amazon.DynamoDBv2.DataModel;
using Domain.Validations;

namespace Domain.Entities
{
    [DynamoDBTable("Accounts")]
    public sealed class Account
    {
        [DynamoDBHashKey("Cpf")]
        public string Cpf { get; set; }
        [DynamoDBProperty]
        public int Agency { get; set; }
        [DynamoDBRangeKey("AccountNumber")]
        public int AccountNumber { get; set; }
        [DynamoDBProperty]
        public decimal Limit { get; set; }

        public Account(string cpf, int agency, int accountNumber, decimal limit)
        {
            Validate(cpf, agency, accountNumber, limit);
        }

        public Account() { }

        public void Validate(string cpf, int agency, int accountNumber, decimal limit)
        {
            EntitieValidator.When(string.IsNullOrEmpty(cpf), "Cpf inválido!");
            EntitieValidator.When(agency <= 0, "O número da agência deve ser maior que 0!");
            EntitieValidator.When(accountNumber <= 0, "O número da conta deve ser maior que 0!");
            EntitieValidator.When(limit < 0, "O limite deve ser maior ou igual a 0!");

            Cpf = cpf;
            Agency = agency;
            AccountNumber = accountNumber;
            Limit = limit;
        }
    }
}
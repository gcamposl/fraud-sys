using Amazon.DynamoDBv2.DataModel;
using Domain.Validations;

namespace Domain.Entities
{
    [DynamoDBTable("Transactions")]
    public sealed class Transaction
    {
        [DynamoDBHashKey("Source")]
        public string Source { get; set; }
        [DynamoDBRangeKey("Destiny")]
        public string Destiny { get; set; }
        [DynamoDBProperty]
        public decimal Value { get; set; }

        public Transaction(string source, string destiny, decimal value)
        {
            Validate(source, destiny, value);
        }

        public Transaction()
        { }

        public void Validate(string source, string destiny, decimal value)
        {
            EntitieValidator.When(string.IsNullOrEmpty(source), "Informe o cpf de origem!");
            EntitieValidator.When(string.IsNullOrEmpty(destiny), "Informe o cpf de destino!");
            EntitieValidator.When(value <= 0, "Valor informado deve ser maior que 0!");

            Source = source;
            Destiny = destiny;
            Value = value;
        }
    }
}
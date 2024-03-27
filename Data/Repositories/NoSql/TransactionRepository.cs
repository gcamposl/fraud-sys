using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories.NoSql
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly IDynamoDBContext _context;

        public TransactionRepository(IDynamoDBContext context, IAmazonDynamoDB dynamoDB)
        {
            _context = context;
            _dynamoDb = dynamoDB;
        }

        public async Task CreateAsync(Transaction transaction)
            => await _context.SaveAsync(transaction);

        public async Task<List<Transaction>> SelectAllAsync()
        {
            var table = Table.LoadTable(_dynamoDb, "Transactions");
            var scanOptions = new ScanOperationConfig();
            var search = table.Scan(scanOptions);
            var transactions = new List<Transaction>();

            do
            {
                var documentList = await search.GetNextSetAsync();
                foreach (var document in documentList)
                {
                    transactions.Add(DocumentToAccount(document));
                }
            } while (!search.IsDone);

            return transactions;
        }
        private static Transaction DocumentToAccount(Document document)
            => new Transaction(
                document["Source"].ToString() ?? "Cpf de origem Inválido!",
                document["Destiny"].ToString() ?? "Cpf de destino Inválido!",
                (decimal)document["Value"]);
    }
}
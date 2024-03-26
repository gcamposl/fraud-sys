using System.Xml.Linq;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories.NoSql
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly IDynamoDBContext _context;

        public AccountRepository(IAmazonDynamoDB dynamoDb, IDynamoDBContext context)
        {
            _dynamoDb = dynamoDb;
            _context = context;
        }

        public async Task CreateAsync(Account account)
            => await _context.SaveAsync(account);

        public async Task<IEnumerable<Account>> SelectAllAsync()
        {
            //TODO: simplicar este método!
            var table = Table.LoadTable(_dynamoDb, "Accounts");
            var scanOptions = new ScanOperationConfig();
            var search = table.Scan(scanOptions);
            var accounts = new List<Account>();

            do
            {
                var documentList = await search.GetNextSetAsync();
                foreach (var document in documentList)
                {
                    accounts.Add(DocumentToAccount(document));
                }
            } while (!search.IsDone);

            return accounts;
        }

        public async Task<ICollection<Account>> SelectByCpfAsync(string cpf)
            => await _context.QueryAsync<Account>(cpf).GetRemainingAsync();

        public async Task UpdateAsync(Account account)
            => await _context.SaveAsync(account);

        public async Task DeleteByCpfAsync(string cpf, int accountNumber)
            => await _context.DeleteAsync<Account>(cpf, accountNumber);

        private static Account DocumentToAccount(Document document)
            => new Account(
                document["Cpf"].ToString() ?? "Inválido!",
                (int)document["Agency"],
                (int)document["AccountNumber"],
                (decimal)document["Limit"]);
    }
}
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories.NoSql
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IAmazonDynamoDB _dynamoDb;

        public AccountRepository(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
        }

        public async Task<Account> CreateAsync(Account account)
        {
            var table = Table.LoadTable(_dynamoDb, "Accounts");
            var document = new Document
            {
                ["Cpf"] = account.Cpf,
                ["Agency"] = account.Agency,
                ["AccountNumber"] = account.AccountNumber,
                ["Limit"] = account.Limit
            };

            await table.PutItemAsync(document);

            return account;
        }

        public async Task<ICollection<Account>> SelectAllAsync()
        {
            //TODO: simplicar este método!!!
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

        public async Task<Account> SelectByCpfAsync(string cpf)
        {
            var table = Table.LoadTable(_dynamoDb, "Accounts");
            var document = await table.GetItemAsync(cpf);

            return document != null ? DocumentToAccount(document) : null;
        }

        public async Task UpdateAsync(Account account)
        {
            var table = Table.LoadTable(_dynamoDb, "Accounts");
            var document = new Document
            {
                ["Cpf"] = account.Cpf,
                ["Agency"] = account.Agency,
                ["AccountNumber"] = account.AccountNumber,
                ["Limit"] = account.Limit
            };

            await table.UpdateItemAsync(document);
        }

        public async Task DeleteByCpfAsync(string cpf)
        {
            var table = Table.LoadTable(_dynamoDb, "Accounts");
            await table.DeleteItemAsync(cpf);
        }

        private static Account DocumentToAccount(Document document)
            => new Account(
                document["Cpf"].ToString() ?? "Inválido!",
                (int)document["Agency"],
                (int)document["AccountNumber"],
                (decimal)document["Limit"]);
    }
}
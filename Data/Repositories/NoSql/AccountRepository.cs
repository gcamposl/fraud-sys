using Amazon.DynamoDBv2;
using Domain.DTOs;
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

        public async Task<Account> CreateAsync(Account accountDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Account>> SelectAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Account> SelectByCpfAsync(string cpf)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Account accountDTO)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByCpfAsync(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
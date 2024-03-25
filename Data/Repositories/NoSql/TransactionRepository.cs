using Amazon.DynamoDBv2.DataModel;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories.NoSql
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDynamoDBContext _context;

        public TransactionRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Transaction transaction)
            => await _context.SaveAsync(transaction);
    }
}
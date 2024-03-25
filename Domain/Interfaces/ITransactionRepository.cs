using Domain.DTOs;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task CreateAsync(Transaction transactionDTO);
    }
}
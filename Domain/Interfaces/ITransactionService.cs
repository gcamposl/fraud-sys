using Domain.DTOs;
using Domain.Services;

namespace Domain.Interfaces
{
    public interface ITransactionService
    {
        Task<ResultService> CreateAsync(TransactionDTO transactionDTO);
    }
}
using Domain.DTOs;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAsync(AccountDTO account);
        Task<ICollection<Account>> SelectAllAsync();
        Task<Account> SelectByCpfAsync(string cpf);
        Task<Account> UpdateAsync(AccountDTO account);
        Task<Account> DeleteByCpfAsync(string cpf);

    }
}
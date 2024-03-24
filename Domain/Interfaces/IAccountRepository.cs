using Domain.DTOs;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAsync(Account account);
        Task<ICollection<Account>> SelectAllAsync();
        Task<Account> SelectByCpfAsync(string cpf);
        Task UpdateAsync(Account account);
        Task DeleteByCpfAsync(string cpf);

    }
}
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task CreateAsync(Account account);
        Task<IEnumerable<Account>> SelectAllAsync();
        Task<Account> SelectByCpfAsync(string cpf);
        Task UpdateAsync(Account account);
        Task DeleteByCpfAsync(string cpf);

    }
}
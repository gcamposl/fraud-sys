using Domain.DTOs;
using Domain.Entities;
using Domain.Services;

namespace Domain.Interfaces
{
    public interface IAccountService
    {
        Task<ResultService<AccountDTO>> CreateAsync(AccountDTO accountDTO);
        Task<ResultService<IEnumerable<AccountDTO>>> GetAllAsync();
        Task<ResultService<AccountDTO>> GetByCpfAsync(string cpf);
        Task<ResultService> UpdateAsync(AccountDTO accountDTO);
        Task<ResultService> DeleteAsync(string cpf);
    }
}
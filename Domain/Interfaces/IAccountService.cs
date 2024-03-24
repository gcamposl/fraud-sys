using Domain.DTOs;
using Domain.Services;

namespace Domain.Interfaces
{
    public interface IAccountService
    {
        Task<ResultService<AccountDTO>> CreateAsync(AccountDTO accountDTO);
        Task<ResultService<ICollection<AccountDTO>>> GetAllAsync();
        Task<ResultService<AccountDTO>> GetByCpfAsync(string cpf);
        Task<ResultService<AccountDTO>> UpdateAsync(AccountDTO account);
        Task<ResultService<bool>> DeleteAsync(string cpf);

    }
}
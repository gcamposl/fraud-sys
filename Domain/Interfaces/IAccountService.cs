using Domain.DTOs;
using Domain.Services;

namespace Domain.Interfaces
{
    public interface IAccountService
    {
        Task<ResultService<AccountDTO>> CreateAsync(AccountDTO accountDTO);
    }
}
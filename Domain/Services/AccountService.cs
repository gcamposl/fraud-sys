using Domain.DTOs;
using Domain.Interfaces;
using Domain.Validations;

namespace Domain.Services
{
    public class AccountService : IAccountService
    {

        public async Task<ResultService<AccountDTO>> CreateAsync(AccountDTO accountDTO)
        {
            if (accountDTO == null)
                return ResultService.Fail<AccountDTO>("Objeto vazio!");

            var result = new AccountDTOValidator().Validate(accountDTO);
            if (!result.IsValid)
                return ResultService.RequestError<AccountDTO>("Problema na validação dos dados!", result);

            //TODO: refact para autoMapper - mesmo sendo menos performatico
            var account = new AccountDTO
            {
                Cpf = accountDTO.Cpf,
                AgencyNumber = accountDTO.AgencyNumber,
                AccountNumber = accountDTO.AccountNumber,
                Limit = accountDTO.Limit
            };

            return ResultService.Ok<AccountDTO>(account);
        }
    }
}
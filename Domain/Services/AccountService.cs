using Domain.DTOs;
using Domain.Entities;
using Domain.Validations;
using Domain.Interfaces;

namespace Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ResultService> CreateAsync(AccountDTO accountDTO)
        {
            if (!CpfValidator.IsValidCpf(accountDTO.Cpf))
                return ResultService.Fail<AccountDTO>("CpfInválido!");

            var result = new AccountDTOValidator().Validate(accountDTO);
            if (!result.IsValid)
                return ResultService.RequestError<AccountDTO>("Problema na validação dos dados!", result);

            await _accountRepository.CreateAsync(MappingToEntitie(accountDTO));

            return ResultService.Ok(accountDTO);
        }

        public async Task<ResultService<List<AccountDTO>>> GetAllAsync()
        {
            var accounts = await _accountRepository.SelectAllAsync();
            var listOfAccountsDto = new List<AccountDTO>();

            foreach (var account in accounts)
                listOfAccountsDto.Add(MappingToDto(account));

            return ResultService.Ok(listOfAccountsDto);
        }

        public async Task<ResultService<AccountDTO>> GetByCpfAsync(string cpf)
        {
            var account = await _accountRepository.SelectByCpfAsync(cpf);
            if (account is null)
                return ResultService.Fail<AccountDTO>("Conta inexistente!");

            return ResultService.Ok(MappingToDto(account.First()));
        }

        public async Task<ResultService> UpdateAsync(AccountDTO accountDTO)
        {
            var validation = new AccountDTOValidator().Validate(accountDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Os dados informados estão inválidos!", validation);

            var account = await _accountRepository.SelectByCpfAsync(accountDTO.Cpf);
            if (account is null)
                return ResultService.Fail($"Cpf - {accountDTO.Cpf} não possui conta!");

            // usando o dynamoDBClient fica muito pesado...
            await _accountRepository.DeleteByCpfAsync(account.First().Cpf, account.First().AccountNumber);
            await _accountRepository.UpdateAsync(MappingToEntitie(accountDTO));

            return ResultService.Ok($"Dados da conta atualizados!");
        }

        public async Task<ResultService> DeleteAsync(string cpf, int accountNumber)
        {
            var account = await _accountRepository.SelectByCpfAsync(cpf);
            if (account is null)
                return ResultService.Fail("Conta inexistente!");

            await _accountRepository.DeleteByCpfAsync(cpf, accountNumber);

            return ResultService.Ok($"Conta removida!");
        }

        // Não utilizei autoMapper por conta de performance
        private static Account MappingToEntitie(AccountDTO accountDTO)
            => new Account(accountDTO.Cpf, accountDTO.Agency, accountDTO.AccountNumber, accountDTO.Limit);

        private static AccountDTO MappingToDto(Account account)
            => new AccountDTO { Cpf = account.Cpf, Agency = account.Agency, AccountNumber = account.AccountNumber, Limit = account.Limit };
    }
}
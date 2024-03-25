using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Validations;

namespace Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<AccountDTO>> CreateAsync(AccountDTO accountDTO)
        {
            if (accountDTO == null)
                return ResultService.Fail<AccountDTO>("Preencha os dados da Conta!");

            var result = new AccountDTOValidator().Validate(accountDTO);
            if (!result.IsValid)
                return ResultService.RequestError<AccountDTO>("Problema na validação dos dados!", result);

            var account = _mapper.Map<Account>(accountDTO);
            await _accountRepository.CreateAsync(account);

            return ResultService.Ok<AccountDTO>(accountDTO);
        }

        public async Task<ResultService<IEnumerable<AccountDTO>>> GetAllAsync()
        {
            var account = await _accountRepository.SelectAllAsync();

            return ResultService.Ok<IEnumerable<AccountDTO>>(_mapper.Map<ICollection<AccountDTO>>(account));
        }

        public async Task<ResultService<AccountDTO>> GetByCpfAsync(string cpf)
        {
            var account = await _accountRepository.SelectByCpfAsync(cpf);
            if (account is null)
                return ResultService.Fail<AccountDTO>("Conta inexistente!");

            return ResultService.Ok<AccountDTO>(_mapper.Map<AccountDTO>(account));
        }

        public async Task<ResultService> UpdateAsync(AccountDTO accountDTO)
        {
            if (accountDTO is null)
                return ResultService.Fail("Os dados da conta devem ser informados!");

            var validation = new AccountDTOValidator().Validate(accountDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Os dados informados estão inválidos!", validation);

            var account = await _accountRepository.SelectByCpfAsync(accountDTO.Cpf);
            if (account is null)
                return ResultService.Fail($"Cpf - {accountDTO.Cpf} não possui conta!");

            account = _mapper.Map<AccountDTO, Account>(accountDTO, account);
            await _accountRepository.UpdateAsync(account);

            return ResultService.Ok($"Dados da conta atualizados! Cpf: {account.Cpf}");
        }

        public async Task<ResultService> DeleteAsync(string cpf)
        {
            var account = await _accountRepository.SelectByCpfAsync(cpf);
            if (account is null)
                return ResultService.Fail("Conta inexistente!");

            await _accountRepository.DeleteByCpfAsync(cpf);

            return ResultService.Ok($"Conta removida!");
        }
    }
}
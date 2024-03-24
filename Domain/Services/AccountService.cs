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
                return ResultService.Fail<AccountDTO>("Objeto vazio!");

            var result = new AccountDTOValidator().Validate(accountDTO);
            if (!result.IsValid)
                return ResultService.RequestError<AccountDTO>("Problema na validação dos dados!", result);

            var account = _mapper.Map<Account>(accountDTO);
            var data = await _accountRepository.CreateAsync(account);
            return ResultService.Ok<AccountDTO>(_mapper.Map<AccountDTO>(data));
        }

        public async Task<ResultService<ICollection<AccountDTO>>> GetAllAsync()
        {
            var account = await _accountRepository.SelectAllAsync();
            return ResultService.Ok<ICollection<AccountDTO>>(_mapper.Map<ICollection<AccountDTO>>(account));
        }

        public async Task<ResultService<AccountDTO>> GetByCpfAsync(string cpf)
        {
            var account = await _accountRepository.SelectByCpfAsync(cpf);
            if (account is null)
                return ResultService.Fail<AccountDTO>("Conta inexistente");

            return ResultService.Ok<AccountDTO>(_mapper.Map<AccountDTO>(account));
        }

        public Task<ResultService<AccountDTO>> UpdateAsync(AccountDTO account)
        {
            throw new NotImplementedException();
        }
        public Task<ResultService<bool>> DeleteAsync(string cpf)
        {
            throw new NotImplementedException();
        }

    }
}
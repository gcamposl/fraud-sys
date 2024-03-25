using System.Transactions;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Validations;

namespace Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(IAccountService accountService, IAccountRepository accountRepository, ITransactionRepository transactionRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<ResultService> CreateAsync(TransactionDTO transactionDTO)
        {
            // inicia transação do pix
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                if (transactionDTO is null)
                    return ResultService.Fail<AccountDTO>("Preencha os dados da transferência!");

                var result = new TransactionDTOValidator().Validate(transactionDTO);
                if (!result.IsValid)
                    return ResultService.RequestError<TransactionDTO>("Problema na validação dos dados!", result);

                var sourceAccount = await _accountRepository.SelectByCpfAsync(transactionDTO.Source);
                if (sourceAccount is null)
                    return ResultService.Fail("Conta de origem não encontrada!");

                if (sourceAccount.Limit < transactionDTO.Value)
                    return ResultService.Fail("Valor indisponível na conta!");

                var destinyAccount = await _accountRepository.SelectByCpfAsync(transactionDTO.Destiny);
                if (destinyAccount is null)
                    return ResultService.Fail("Conta de destino não encontrada!");

                var account = new AccountDTO
                {
                    Cpf = sourceAccount.Cpf,
                    Agency = sourceAccount.Agency,
                    AccountNumber = sourceAccount.AccountNumber,
                    Limit = sourceAccount.Limit - transactionDTO.Value
                };

                await _accountService.UpdateAsync(account);

                var transaction = _mapper.Map<TransactionDTO, Entities.Transaction>(transactionDTO);
                await _transactionRepository.CreateAsync(transaction);

                // confirma a transação se todas as operações foram bem sucedidas
                transactionScope.Complete();

                return ResultService.Ok("Pix realizado com sucesso!");
            }
            catch (Exception ex)
            {
                // caso haja algum erro, rollback + mensagem.
                return ResultService.Fail("Ocorreu um erro ao processar a transação: " + ex.Message);
            }
        }
    }
}

using System.Transactions;
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

        public TransactionService(IAccountService accountService, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _accountService = accountService;
        }

        public async Task<ResultService> CreateAsync(TransactionDTO transactionDTO)
        {
            // inicia transação do pix
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var result = new TransactionDTOValidator().Validate(transactionDTO);
                if (!result.IsValid)
                    return ResultService.RequestError<TransactionDTO>("Problema na validação dos dados!", result);

                var sourceAccountList = await _accountRepository.SelectByCpfAsync(transactionDTO.Source);
                var sourceAccount = sourceAccountList.First();
                if (sourceAccount is null)
                    return ResultService.Fail("Conta de origem não encontrada!");

                if (sourceAccount.Limit < transactionDTO.Value)
                    return ResultService.Fail("Valor indisponível na conta!");

                var destinyAccountList = await _accountRepository.SelectByCpfAsync(transactionDTO.Destiny);
                var destinyAccount = destinyAccountList.First();
                if (destinyAccount is null)
                    return ResultService.Fail("Conta de destino não encontrada!");

                sourceAccount.Limit -= transactionDTO.Value;
                destinyAccount.Limit += transactionDTO.Value;
                var accounts = new List<Account>
                {
                    sourceAccount,
                    destinyAccount
                };

                await UpdateLimitAccountAsync(accounts);
                await _transactionRepository.CreateAsync(MappingToEntitie(transactionDTO));

                // confirma a transação se todas as operações foram bem sucedidas
                transactionScope.Complete();

                return ResultService.Ok("Pix realizado com sucesso!");
            }
            catch (Exception ex)
            {
                return ResultService.Fail("Ocorreu um erro ao processar a transação: " + ex.Message);
            }
        }

        public async Task<ResultService<List<TransactionDTO>>> GetAllAsync()
        {
            var transactions = await _transactionRepository.SelectAllAsync();
            var listOfTransactionsDto = new List<TransactionDTO>();
            foreach (var transaction in transactions)
                listOfTransactionsDto.Add(MappingToDto(transaction));

            return ResultService.Ok(listOfTransactionsDto);
        }

        private async Task UpdateLimitAccountAsync(ICollection<Account> accounts)
        {
            foreach (var account in accounts)
                await _accountService.UpdateAsync(MappingToEntitie(account, account.Limit));
        }

        private Entities.Transaction MappingToEntitie(TransactionDTO transactionDTO)
            => new(transactionDTO.Source, transactionDTO.Destiny, transactionDTO.Value);
        private static TransactionDTO MappingToDto(Entities.Transaction transaction)
            => new TransactionDTO { Source = transaction.Source, Destiny = transaction.Destiny, Value = transaction.Value };

        private AccountDTO MappingToEntitie(Account account, decimal newLimit)
            => new AccountDTO
            {
                Cpf = account.Cpf,
                Agency = account.Agency,
                AccountNumber = account.AccountNumber,
                Limit = newLimit
            };
    }
}

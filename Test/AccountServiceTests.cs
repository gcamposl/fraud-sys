using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IAccountRepository> _mockAccountRepository;
        private IMapper _mapper;
        private IAccountService _accountService;

        [SetUp]
        public void Setup()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Account, AccountDTO>();
                cfg.CreateMap<AccountDTO, Account>();
            });
            _mapper = configuration.CreateMapper();
            _accountService = new AccountService(_mockAccountRepository.Object, _mapper);
        }

        [Test]
        public async Task CreateAsync_ValidAccountDTO_ReturnsSuccessResult()
        {
            // Arrange
            var accountDTO = new AccountDTO
            {
                Cpf = "12345678901",
                Agency = 1234,
                AccountNumber = 56789,
                Limit = 1000
            };
            _mockAccountRepository.Setup(repo => repo.CreateAsync(It.IsAny<Account>())).Returns(Task.CompletedTask);

            // Act
            var result = await _accountService.CreateAsync(accountDTO);

            // Assert
            Assert.That(result.IsSucces);
            Assert.Equals("12345678901", result.Data.Cpf);
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfAccountDTOs()
        {
            // Arrange
            var accounts = new List<Account>
            {
                new Account("12345678901", 1234, 56789, 1000),
                new Account("98765432109", 5678, 98765, 2000)
            };
            _mockAccountRepository.Setup(repo => repo.SelectAllAsync()).ReturnsAsync(accounts);

            // Act
            var result = await _accountService.GetAllAsync();

            // Assert
            Assert.That(result.IsSucces);
            Assert.Equals(2, result.Data.Count());
        }
    }
}

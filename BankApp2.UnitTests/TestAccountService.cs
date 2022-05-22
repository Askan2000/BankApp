using AutoMapper;
using BankApp2.Core.Interfaces;
using BankApp2.Core.Services;
using BankApp2.Data.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace BankApp2.UnitTests
{
    public class TestAccountService
    {
        //Mockning av injectade grejer

        private readonly IAccountService _accountService;
        private readonly Mock<IAccountRepo> _accountRepo = new Mock<IAccountRepo>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<ILogger<AccountService>> _logger = new Mock<ILogger<AccountService>>();

        public TestAccountService()
        {
            _accountService = new AccountService(_mapper.Object, _accountRepo.Object, _logger.Object);
        }

        [Fact]
        public async Task TestGetAccount_throwsException()
        {
            //Id får inte vara mindre än 1
            int id = 0;

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>( () => _accountService.GetAccount(id));
        }

        [Fact]
        public async Task TestUpdateAccount_throwsException()
        {
            //accountId eller amount får inte vara mindre än 1
            int accountId = -1;
            decimal amount = 0;

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _accountService.UpdateAccount(accountId, amount));

        }
    }
}
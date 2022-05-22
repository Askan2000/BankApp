using AutoMapper;
using BankApp2.Core.Interfaces;
using BankApp2.Core.Services;
using BankApp2.Data.Interfaces;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.UnitTests
{
    public class TestAccountTypeService
    {
        //Mockning av injectade grejer

        private readonly IAccountTypeService _accountTypeService;
        private readonly Mock<IAccountTypeRepo> _accountTypeRepo = new Mock<IAccountTypeRepo>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<ILogger<AccountTypeService>> _logger = new Mock<ILogger<AccountTypeService>>();

        public TestAccountTypeService()
        {
            _accountTypeService = new AccountTypeService(_accountTypeRepo.Object, _mapper.Object, _logger.Object);
        }

        [Fact]
        public async Task TestGetAccount_throwsException()
        {
            //AccountTypeDto får inte vara null
            AccountTypeDto accountTypeDto = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _accountTypeService.CreateAccountType(accountTypeDto));
        }
    }
}

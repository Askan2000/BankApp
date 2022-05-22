using BankApp2.Core.Interfaces;
using BankApp2.Core.Services;
using BankApp2.Data.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.UnitTests
{
    public class TestDispositionService
    {
        private readonly IDispositionService _dispositionService;
        private readonly Mock<IDispositionRepo> _dispositionRepo = new Mock<IDispositionRepo>();
        private readonly Mock<ILogger<DispositionService>> _logger = new Mock<ILogger<DispositionService>>();

        public TestDispositionService()
        {
            _dispositionService = new DispositionService(_dispositionRepo.Object, _logger.Object);
        }

        [Fact]
        public async Task TestCreateDisposition_throwsException()
        {
            int CustomerId = 1;
            int AccountId = 1;
            //Får inte vara ""
            string dispositionType = "";

            await Assert.ThrowsAsync<Exception>( () => _dispositionService.CreateDisposition(CustomerId, AccountId, dispositionType));
        }
    }
}

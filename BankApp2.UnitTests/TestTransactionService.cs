using BankApp2.Core.Interfaces;
using BankApp2.Core.Services;
using BankApp2.Data.Interfaces;
using BankApp2.Shared.Models;
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
    public class TestTransactionService
    {
        private readonly ITransactionService _transactionService;
        private readonly Mock<ITransactionRepo> _transactionRepo = new Mock<ITransactionRepo>();
        private readonly Mock<IAccountRepo> _accountRepo = new Mock<IAccountRepo>();
        private readonly Mock<ILogger<TransactionService>> _logger = new Mock<ILogger<TransactionService>>();

        public TestTransactionService()
        {
            _transactionService = new TransactionService(_transactionRepo.Object, _accountRepo.Object, _logger.Object);
        }

        //Testar att funktionen CreateTransaction i service-lagret kallar på metoder i repot och returnerar ett Transaction-objekt
        [Fact]
        public void TestCreateTransaction_returnCreatedTransaction()
        {
            Transaction returnedTransaction = new Transaction();

            //Setup Mock
            _transactionRepo
                .Setup(x => x.PostTransaction(It.IsAny<Transaction>())).ReturnsAsync((Task<Transaction> transaction) => returnedTransaction);

            _transactionRepo
                .Setup(x => x.GetTransaction(It.IsAny<int>())).ReturnsAsync((Task<Transaction> transaction) => returnedTransaction);

            var result = _transactionService.CreateTransaction(1, 1, "transactiontype", "transactionoperation");

            Assert.IsType<Task<Transaction>>(result);
        }

        [Fact]
        public async Task TestCreateTransaction_throwException()
        {
            NewTransaction transaction = new NewTransaction();
            transaction.Amount = "-1";

            await Assert.ThrowsAsync<Exception>(() => _transactionService.CreateTransaction(transaction));
        }
        [Fact]
        public async Task TestCreateTransactionOverload_throwException()
        {
            int accountId = 10;
            decimal amount = 1000;
            //Får inte vara ""
            string transactionType = "";
            //Får inte vara null
            string transactionOperation = null;

            await Assert.ThrowsAsync<Exception>(() => _transactionService.CreateTransaction(accountId, amount, transactionType, transactionOperation));
        }

        
    }
}

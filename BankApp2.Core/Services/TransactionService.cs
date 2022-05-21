using BankApp2.Core.Interfaces;
using BankApp2.Data.Interfaces;
using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IAccountRepo _accountRepo;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(ITransactionRepo transactionRepo, IAccountRepo accountRepo, ILogger<TransactionService> logger)
        {
            _transactionRepo = transactionRepo;
            _accountRepo = accountRepo;
            _logger = logger;
        }

        public async Task<NewTransaction> CreateTransaction(NewTransaction transaction)
        {
            try
            {
                //Här mappar jag om till ett Transaction-objekt och skickar vidare till repot
                //Först skapas senderTransaction
                Transaction senderTransaction = new Transaction();

                senderTransaction.AccountId = int.Parse(transaction.SenderAccountId);
                senderTransaction.Date = DateTime.Now.Date;
                senderTransaction.Type = "Debit";
                senderTransaction.Operation = "Bank Transfer";
                senderTransaction.Amount = -1 * (decimal)int.Parse(transaction.Amount);

                //För Balance behöver jag hämta upp nuvarande balance för båda AccountId och dra av/lägga på överföringssumman
                var lastTransactionSender = await GetTransaction(int.Parse(transaction.SenderAccountId));
                var lastTransactionReciever = await GetTransaction(int.Parse(transaction.RecieverAccountId));

                senderTransaction.Balance = lastTransactionSender.Balance - int.Parse(transaction.Amount);

                //Sen skapas recieverTransaction
                Transaction recieverTransaction = new Transaction();
                recieverTransaction.AccountId = int.Parse(transaction.RecieverAccountId);
                recieverTransaction.Date = DateTime.Now.Date;
                recieverTransaction.Type = "Credit";
                recieverTransaction.Operation = "Bank Transfer";
                recieverTransaction.Amount = (decimal)int.Parse(transaction.Amount);

                //Om mottagaren har haft tidigare transaktioner, lägg på summan som ska överföras bara, om inte, sätt bara reciverBalance till Amount
                if(lastTransactionReciever != null)
                {
                    recieverTransaction.Balance = lastTransactionReciever.Balance + int.Parse(transaction.Amount);
                }
                else
                {
                    recieverTransaction.Balance = int.Parse(transaction.Amount);
                }

                //Genomför transaktionerna om ovan gick bra
                var resultSender = await _transactionRepo.PostTransaction(senderTransaction);
                var resultReciever = await _transactionRepo.PostTransaction(recieverTransaction);

                //Uppdatera sedan balance på Account för både Sender och Reciever
                var accountSender = await _accountRepo.UpdateAccount(senderTransaction.AccountId, senderTransaction.Amount);
                var accountReciever = await _accountRepo.UpdateAccount(recieverTransaction.AccountId, recieverTransaction.Amount);

                //Egentligen tokigt, här skickar jag bara tillbaka det inkommande objektet NewTransaction
                return transaction;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateTransaction)} service method {ex} ");
                throw;
            }
            
        }

        public async Task<Transaction> CreateTransaction(int accountId, decimal amount, string transactionType, string transactionOperation)
        {
            try
            {
                Transaction transaction = new Transaction();
                transaction.AccountId = accountId;
                transaction.Date = DateTime.Now.Date;
                transaction.Type = transactionType;
                transaction.Operation = transactionOperation;
                transaction.Amount = amount;

                var latestTransaction = await _transactionRepo.GetTransaction(accountId);
                if (latestTransaction == null)
                {
                    transaction.Balance = amount;
                }
                else
                {
                    transaction.Balance = latestTransaction.Balance + amount;
                }

                return await _transactionRepo.PostTransaction(transaction);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateTransaction)} service method {ex} ");
                throw;
            }         
        }

        public async Task<Transaction> GetTransaction(int accountId)
        {
            try
            {
                return await _transactionRepo.GetTransaction(accountId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetTransaction)} service method {ex} ");
                throw;
            }
        }
    }
}

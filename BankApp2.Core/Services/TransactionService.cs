using BankApp2.Core.Interfaces;
using BankApp2.Data.Interfaces;
using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
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


        public TransactionService(ITransactionRepo transactionRepo, IAccountRepo accountRepo)
        {
            _transactionRepo = transactionRepo;
            _accountRepo = accountRepo;
        }

        public async Task<NewTransaction> CreateTransaction(NewTransaction transaction)
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

            var resultSender = await _transactionRepo.PostTransaction(senderTransaction);

            //Sen skapas recieverTransaction
            Transaction recieverTransaction = new Transaction();
            recieverTransaction.AccountId = int.Parse(transaction.RecieverAccountId);
            recieverTransaction.Date = DateTime.Now.Date;
            recieverTransaction.Type = "Credit";
            recieverTransaction.Operation = "Bank Transfer";
            recieverTransaction.Amount = (decimal)int.Parse(transaction.Amount);

            recieverTransaction.Balance = lastTransactionReciever.Balance + int.Parse(transaction.Amount);

            var resultReciever = await _transactionRepo.PostTransaction(recieverTransaction);

            //Uppdatera sedan balance på Account för både Sender och Reciever

            var accountSender = await _accountRepo.UpdateAccount(senderTransaction.AccountId, senderTransaction.Balance);
            var accountReciever = await _accountRepo.UpdateAccount(recieverTransaction.AccountId, recieverTransaction.Balance);

            //Egentligen tokigt, här skickar jag bara tillbaka det inkommande objektet NewTransaction
            return transaction;
        }
        public async Task<Transaction> GetTransaction(int accountId)
        {
            return await _transactionRepo.GetTransaction(accountId);
        }
    }
}

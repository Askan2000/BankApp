using BankApp2.Core.Interfaces;
using BankApp2.Shared.Enums;
using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public LoanController(ILoanService loanService, IAccountService accountService, ITransactionService transactionService)
        {
            _loanService = loanService;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> Post(LoanDto loan)
        {
            if (loan == null)
            {
                return BadRequest("Felaktiga låneuppgifter");
            }
            var returnedLoan = await _loanService.CreateLoan(loan);
            if (returnedLoan != null)
            {
                //Om lånet är korrekt upplagt ska samma Amount sättas in på kundens AccountId
                var returnedAccount = await _accountService.UpdateAccount(loan.AccountId, loan.Amount);

                if (returnedAccount != null)
                {
                    //Om uppdateringen av kontosaldot gick bra lägger jag in en transaktion med samma summa 

                    var transactionOperation = "Loan";
                    var returnedTransaction = await _transactionService.CreateTransaction(
                        loan.AccountId, loan.Amount, TransactionTypeEnum.Credit.ToString(), transactionOperation);
                    return Ok(loan);
                }
                else
                {
                    return BadRequest("Gick inte att skapa disposition");
                }
            }
            else
            {
                return BadRequest("Gick inte att skapa konto");
            }

        }
    }
}

using AutoMapper;
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
    public class TestLoanService
    {
        private readonly ILoanService _loanService;
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<ILoanRepo> _loanRepo = new Mock<ILoanRepo>();
        private readonly Mock<ILogger<LoanService>> _logger  = new Mock<ILogger<LoanService>>();

        public TestLoanService()
        {
            _loanService = new LoanService(_loanRepo.Object, _mapper.Object, _logger.Object);
        }

        //Testa att metoden returnerar ett Loan
        [Fact]
        public async Task CreateLoan()
        {
            Loan returnedLoan = new Loan();
            LoanDto loanDto = new LoanDto();
            //Setup Mock

            _loanRepo
                .Setup(x => x.CreateLoan(It.IsAny<Loan>())).ReturnsAsync((Task<Loan> loan) => returnedLoan);

            var result = await _loanService.CreateLoan(loanDto);

            Assert.IsType<Loan>(result);
        }
    }
}

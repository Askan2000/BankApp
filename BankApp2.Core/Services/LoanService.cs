using AutoMapper;
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
    public class LoanService : ILoanService
    {
        private readonly IMapper _mapper;
        private readonly ILoanRepo _loanRepo;
        private readonly ILogger<LoanService> _logger;
        public LoanService(ILoanRepo loanRepo, IMapper mapper, ILogger<LoanService> logger)
        {
            _loanRepo = loanRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Loan> CreateLoan(LoanDto loan)
        {
            try
            {
                var mappedLoan = _mapper.Map<Loan>(loan);

                return await _loanRepo.CreateLoan(mappedLoan);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateLoan)} service method {ex} ");
                throw;
            }
        }
    }
}

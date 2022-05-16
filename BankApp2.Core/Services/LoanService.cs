using AutoMapper;
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
    public class LoanService : ILoanService
    {
        private readonly IMapper _mapper;
        private readonly ILoanRepo _loanRepo;

        public LoanService(ILoanRepo loanRepo, IMapper mapper)
        {
            _loanRepo = loanRepo;
            _mapper = mapper;
        }

        public async Task<Loan> CreateLoan(LoanDto loan)
        {
            var mappedLoan = _mapper.Map<Loan>(loan);

            return await _loanRepo.CreateLoan(mappedLoan);
        }
    }
}

using BankApp2.Core.Interfaces;
using BankApp2.Data.Interfaces;
using BankApp2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Services
{
    public class DispositionService : IDispositionService
    {
        private readonly IDispositionRepo _dispositionRepo;

        public DispositionService(IDispositionRepo dispositionRepo)
        {
            _dispositionRepo = dispositionRepo;
        }

        public async Task<Disposition> CreateDisposition(int CustomerId, int AccountId, string dispositionType)
        {

            Disposition disposition = new Disposition();
            disposition.CustomerId = CustomerId;
            disposition.AccountId = AccountId;
            disposition.Type = dispositionType;

            return await _dispositionRepo.CreateDisposition(disposition);
        }
    }
}

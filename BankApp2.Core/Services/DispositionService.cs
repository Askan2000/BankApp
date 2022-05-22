using BankApp2.Core.Interfaces;
using BankApp2.Data.Interfaces;
using BankApp2.Shared.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<DispositionService> _logger;

        public DispositionService(IDispositionRepo dispositionRepo, ILogger<DispositionService> logger)
        {
            _dispositionRepo = dispositionRepo;
            _logger = logger;
        }

        public async Task<Disposition> CreateDisposition(int CustomerId, int AccountId, string dispositionType)
        {
            if(CustomerId < 1)
                throw new ArgumentOutOfRangeException(nameof(CustomerId));
            if(AccountId < 1)
                throw new ArgumentOutOfRangeException(nameof(AccountId));
            if (dispositionType == "")
                throw new Exception("disposition type måste vara OWNER eller DISPONENT");
            try
            {
                Disposition disposition = new Disposition();
                disposition.CustomerId = CustomerId;
                disposition.AccountId = AccountId;
                disposition.Type = dispositionType;

                return await _dispositionRepo.CreateDisposition(disposition);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateDisposition)} service method {ex} ");
                throw;
            }

        }
    }
}

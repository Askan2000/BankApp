using AutoMapper;
using BankApp2.Core.Interfaces;
using BankApp2.Core.Services;
using BankApp2.Data.Interfaces;
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
    public class TestCustomerService
    {
        //Mockning av injectade grejer

        private readonly ICustomerService _customerService;
        private readonly Mock<ICustomerRepo> _customerRepo = new Mock<ICustomerRepo>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<ILogger<CustomerService>> _logger = new Mock<ILogger<CustomerService>>();

        public TestCustomerService()
        {
            _customerService = new CustomerService(_customerRepo.Object, _mapper.Object, _logger.Object);
        }

        [Fact]
        public async Task TestAddCustomer_throwsException()
        {
            UserRegisterDto user = null;

            string aspNetUserId = null;

            await Assert.ThrowsAsync<ArgumentNullException>(() => _customerService.AddCustomer(user, aspNetUserId));
        }
        [Fact]
        public async Task TestGetCustomer_throwsException()
        {
            //Id får inte vara mindre än 1
            int id = 0;

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _customerService.GetCustomer(id));
        }

    }
}

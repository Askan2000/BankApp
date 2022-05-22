//using BankApp2.Data.Interfaces;
//using BankApp2.Data.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BankApp2.UnitTests
//{
//    public class TestTransactionRepo
//    {
//        private readonly ITransactionRepo transactionRepo;

//        public TestTransactionRepo()
//        {

//            var services = new ServiceCollection();

//            //Lägger på BankContext
//            services.AddDbContext<BankAppDataContext>(options => options.UseSqlServer(
//                Configuration.GetConnectionString("BankConn")));

//            services.AddTransient<IRepo, GetRepo1>();

//            var serviceProvider = services.BuildServiceProvider();

//            _repo = serviceProvider.GetService<IRepo>();
//        }
//    }
//}

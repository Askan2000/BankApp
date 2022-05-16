using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Shared.ModelsNotInDB
{
    public class AccountModel
    {
        public string Frequency { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now.Date;
        public decimal Balance { get; set; } = 0;
        public int AccountTypesId { get; set; } = 1;
        public int CustomerId { get; set; }
        public string DispositionsType { get; set; } = string.Empty;

    }
}

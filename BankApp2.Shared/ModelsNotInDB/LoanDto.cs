using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Shared.ModelsNotInDB
{
    public class LoanDto
    {
        public int AccountId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public  decimal Payments { get; set; } = 0;
        public string Status { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Shared.ModelsNotInDB
{
    public class LoanDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Ange ett giltigt kontonummer, ska börja på 1 och vara max 6 siffror långt")]
        public int AccountId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        [Range(1, 10000000, ErrorMessage = "Ange ett giltigt värde. Lånet får vara mellan 1 kr och 10 MSEK")]
        public decimal Amount { get; set; }
        [Required]
        public int Duration { get; set; } = 12;
        public  decimal Payments { get; set; } = 0;
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}

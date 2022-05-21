using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Shared.ModelsNotInDB
{
    public class AccountModel
    {
        [Required]
        public string Frequency { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now.Date;
        public decimal Balance { get; set; } = 0;
        public int AccountTypesId { get; set; } = 1;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "ANge ett giltigt kundnummer")]
        public int CustomerId { get; set; }
        [Required]
        public string DispositionsType { get; set; } = string.Empty;

    }
}

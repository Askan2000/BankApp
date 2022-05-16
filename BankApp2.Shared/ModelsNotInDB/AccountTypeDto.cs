using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Shared.ModelsNotInDB
{
    public class AccountTypeDto
    {
        [Required]
        public string TypeName { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0.0, 0.5, ErrorMessage = "Värdet måste vara min 0,0 och max 0,5")]
        public decimal Interest { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Shared.ModelsNotInDB
{
    public class NewTransaction
    {
        [Required]
        public string SenderAccountId { get; set; }
        [Required]
        public string RecieverAccountId { get; set; }
        [Required]
        public string Amount { get; set; }



    }
}

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
        [Range(1, 999999, ErrorMessage = "Kontonumret måste börja på 1 och ska vara max 6 siffror")]
        public string RecieverAccountId { get; set; }
        [Required]
        [Range(1, 100000, ErrorMessage = "Värdet måste vara minst 1 kr. Vid överföringar över 100 000 kr kontakta bankkontoret")]
        public string Amount { get; set; }



    }
}

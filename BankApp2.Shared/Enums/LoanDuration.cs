using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Shared.Enums
{
    public enum LoanDuration
    {
        [Display(Name = "12")]
        Twelve,
        [Display(Name = "24")]
        Twentyfour,
        [Display(Name = "36")]
        Thirtysix,
        [Display(Name = "48")]
        Fortyeight
    }
}

using System;
using System.Collections.Generic;

namespace BankApp2.Shared.Models
{
    public partial class AccountType
    {
        public AccountType()
        {
            Accounts = new HashSet<Account>();
        }

        public int AccountTypeId { get; set; }
        public string TypeName { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BankApp2.Shared.Models
{
    public partial class Disposition
    {
        public Disposition()
        {
            Cards = new HashSet<Card>();
        }

        public int DispositionId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; } = null!;

        public virtual Account Account { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Card> Cards { get; set; }
    }
}

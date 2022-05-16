using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankApp2.Shared.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Dispositions = new HashSet<Disposition>();
        }

        public int CustomerId { get; set; }
        [Required]
        public string Gender { get; set; } = null!;
        [Required]
        public string Givenname { get; set; } = null!;
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public string Streetaddress { get; set; } = null!;
        public string City { get; set; } = null!;
        [Required]
        public string Zipcode { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;
        [Required]
        [MaxLength(2)]
        public string CountryCode { get; set; } = null!;
        public DateTime? Birthday { get; set; }
        public string? Telephonecountrycode { get; set; }
        public string? Telephonenumber { get; set; }
        public string? Emailaddress { get; set; }
        public string? AspNetUserId { get; set; }

        public virtual ICollection<Disposition> Dispositions { get; set; }
    }
}

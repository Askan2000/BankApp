using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Shared.ModelsNotInDB
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public string Givenname { get; set; } = string.Empty;
        [Required]
        public string Surname { get; set; } = string.Empty;
        [Required]
        public string StreetAddress { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Zipcode { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
        [Required, MaxLength(2)]
        public string CountryCode { get; set; } = string.Empty;
        
        public string AspnetUserId { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

    }
}

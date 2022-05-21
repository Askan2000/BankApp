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
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Fel format på email")]

        public string Email { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; }
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

using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public class User
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256, ErrorMessage = "Email length cannot exceed 256 characters.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/1900", "12/31/2099", ErrorMessage = "Date of Birth must be a valid date.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s,]+$", ErrorMessage = "Invalid address format.")]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must be exactly 6 digits.")]
        public string Pincode { get; set; }

        public string OTP { get; set; }
        public string Role { get; set; }
    }
}

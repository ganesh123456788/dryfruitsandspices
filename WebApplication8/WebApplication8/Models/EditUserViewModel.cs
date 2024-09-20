using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public class EditUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } // Added for verification

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } // Password to be updated

        [Required]
        public string Role { get; set; }

        [Required]
        public string Gender { get; set; }

        public string FirstName { get; set; } // Add FirstName property
        public string LastName { get; set; }
    }
}

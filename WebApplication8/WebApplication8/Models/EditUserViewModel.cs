using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
=======
using System;
>>>>>>> 901688282898ff11154d4a648ba17e842570c831

namespace WebApplication8.Models
{
    public class EditUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
<<<<<<< HEAD
        public string OldPassword { get; set; } // Added for verification

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } // Password to be updated
=======
        public string Password { get; set; }
>>>>>>> 901688282898ff11154d4a648ba17e842570c831

        [Required]
        public string Role { get; set; }

        [Required]
<<<<<<< HEAD
        public string Gender { get; set; }

        public string FirstName { get; set; } // Add FirstName property
        public string LastName { get; set; }
=======
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
    }
}

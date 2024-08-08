<<<<<<< HEAD
﻿using System;
using System.ComponentModel.DataAnnotations;
=======
﻿// Models/RegisterViewModel.cs
using System.ComponentModel.DataAnnotations;
using System;
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1

namespace WebApplication8.Models
{
    public class User
    {
        [Required]
<<<<<<< HEAD
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
=======
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
<<<<<<< HEAD
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
=======
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Re-enter Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }
<<<<<<< HEAD

        [Required]
        public string Address { get; set; }

        [Required]
        public string Pincode { get; set; }

        public string OTP { get; set; }
        public string Role { get; set; }

=======
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
    }
}

<<<<<<< HEAD
﻿using System;
using System.ComponentModel.DataAnnotations;
=======
<<<<<<< HEAD
﻿using System;
using System.ComponentModel.DataAnnotations;
=======
﻿// Models/RegisterViewModel.cs
using System.ComponentModel.DataAnnotations;
using System;
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e

namespace WebApplication8.Models
{
    public class User
    {
        [Required]
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
<<<<<<< HEAD
=======
=======
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
<<<<<<< HEAD
=======
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
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e

        [Required]
        public string Address { get; set; }

        [Required]
        public string Pincode { get; set; }

        public string OTP { get; set; }
        public string Role { get; set; }

<<<<<<< HEAD
=======
=======
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
    }
}

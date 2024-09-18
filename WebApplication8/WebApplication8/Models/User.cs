<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
﻿using System;
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
<<<<<<< HEAD
﻿using System;
using System.ComponentModel.DataAnnotations;
=======
<<<<<<< HEAD
﻿using System;
using System.ComponentModel.DataAnnotations;
=======
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
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831

namespace WebApplication8.Models
{
    public class User
    {
        [Required]
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda

        [Required]
        [EmailAddress]
        [StringLength(256, ErrorMessage = "Email length cannot exceed 256 characters.")]
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831

        [Required]
        [EmailAddress]
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
<<<<<<< HEAD
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
=======
<<<<<<< HEAD
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
=======
<<<<<<< HEAD
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831

        [Required]
        public string Address { get; set; }

        [Required]
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
        public string Pincode { get; set; }

        public string OTP { get; set; }
        public string Role { get; set; }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
    }
}

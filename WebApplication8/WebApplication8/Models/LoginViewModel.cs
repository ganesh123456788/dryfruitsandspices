<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
﻿// Models/LoginViewModel.cs
using System.ComponentModel.DataAnnotations;
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1

namespace WebApplication8.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

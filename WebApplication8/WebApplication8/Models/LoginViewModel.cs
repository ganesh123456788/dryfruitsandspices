<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
﻿// Models/LoginViewModel.cs
using System.ComponentModel.DataAnnotations;
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831

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

using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public class VerifyOtpViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "OTP")]
        public string Otp { get; set; }
    }
}

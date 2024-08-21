using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public class RequestOtpViewModel
    {
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}

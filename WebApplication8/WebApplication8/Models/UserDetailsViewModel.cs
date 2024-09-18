namespace WebApplication8.Models
{
    public class UserDetailsViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }  // Consider masking or not showing the password
        public string Role { get; set; }
    }
}

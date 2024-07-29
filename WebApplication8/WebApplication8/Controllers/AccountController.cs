// Controllers/AccountController.cs
using WebApplication8.Models;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
namespace WebApplication8.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-5M6SBGL;Initial Catalog=UserDatabase;Integrated Security=True"))
                {
                    conn.Open();
                    string query = "SELECT PasswordHash FROM Users WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", model.Email);

                    var passwordHash = (string)cmd.ExecuteScalar();

                    if (passwordHash != null && VerifyPasswordHash(model.Password, passwordHash))
                    {
                        // Successful login
                        return RedirectToAction("ddryfruitsandspices", "Home");
                    }
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-5M6SBGL;Initial Catalog=UserDatabase;Integrated Security=True"))
                {
                    conn.Open();
                    string query = "INSERT INTO Users (FullName, Email, PasswordHash, DateOfBirth, Gender) VALUES (@FullName, @Email, @PasswordHash, @DateOfBirth, @Gender)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", HashPassword(model.Password));
                    cmd.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", model.Gender);

                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            string hashOfInput = HashPassword(password);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashOfInput, storedHash) == 0;
        }
    }
}

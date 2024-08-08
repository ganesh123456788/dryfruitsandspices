using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Password, Role FROM UserRegistrationDB WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", model.Email);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHashedPassword = reader["Password"].ToString();
                                string role = reader["Role"].ToString();

                                // Verify the provided password
                                if (BCrypt.Net.BCrypt.Verify(model.Password, storedHashedPassword))
                                {
                                    // Redirect based on role
                                    if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                                    {
                                        return RedirectToAction("ddryfruitsandspices", "Home");
                                    }
                                    else if (role.Equals("user", StringComparison.OrdinalIgnoreCase))
                                    {
                                        return RedirectToAction("adminpage", "Home");
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "Role is not recognized.");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Invalid email or password.");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "Invalid email or password.");
                            }
                        }
                    }
                }
            }

            return View(model);
        }
    }
}

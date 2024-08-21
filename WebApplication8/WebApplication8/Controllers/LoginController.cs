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
                                    // Store user details in TempData
                                    TempData["Email"] = model.Email;
                                    TempData["Password"] = model.Password;
                                    TempData["Role"] = role;

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

        [HttpGet]
        public ActionResult EditUser()
        {
            var email = TempData["Email"] as string;

            if (email == null)
            {
                return RedirectToAction("Login");
            }

            string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Email, Password, Role, DateOfBirth, Gender FROM UserRegistrationDB WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var editUserModel = new EditUserViewModel
                            {
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                Role = reader["Role"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                Gender = reader["Gender"].ToString()
                            };

                            return View(editUserModel);
                        }
                    }
                }
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    string query = "UPDATE UserRegistrationDB SET Email = @Email, Password = @Password, Role = @Role, DateOfBirth = @DateOfBirth, Gender = @Gender WHERE Email = @OldEmail";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", model.Email);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@Role", model.Role);
                        command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", model.Gender);
                        command.Parameters.AddWithValue("@OldEmail", TempData["Email"].ToString());
                        connection.Open();
                        command.ExecuteNonQuery();
                        // Update TempData with new values
                        TempData["Email"] = model.Email;
                        TempData["Password"] = model.Password;
                        TempData["Role"] = model.Role;
                        return RedirectToAction("adminpage", "Home");
                    }
                }
            }

            return View(model);
        }
    }
}

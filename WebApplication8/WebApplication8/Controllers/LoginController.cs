using System;
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Mvc;
using WebApplication8.Models;
using BCrypt.Net;
using System.Configuration;
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using WebApplication8.Models;
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b

namespace WebApplication8.Controllers
{
    public class LoginController : Controller
    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
        }

        // GET: Login
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

<<<<<<< HEAD
        // POST: Login
=======
<<<<<<< HEAD
        // POST: Login
=======
<<<<<<< HEAD
        // POST: Login
=======
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                try
                {
                    using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                    {
                        string query = "SELECT Password, Role, FirstName, LastName FROM UserRegistrationDB WHERE Email = @Email";
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
                                    string firstName = reader["FirstName"].ToString();
                                    string lastName = reader["LastName"].ToString();

                                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, storedHashedPassword);

                                    if (isPasswordValid)
                                    {
                                        Session["Email"] = model.Email;
                                        Session["Role"] = role;
                                        Session["FirstName"] = firstName;
                                        Session["LastName"] = lastName;

                                        if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                                        {
                                            return RedirectToAction("ddryfruitsandspices", "Home");
                                        }
                                        else if (role.Equals("user", StringComparison.OrdinalIgnoreCase))
                                        {
                                            return RedirectToAction("Index", "Combined");
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "Role is not recognized.");
                                        }
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "Invalid email or password.");
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
                                    // Store user details in TempData
                                    TempData["Email"] = model.Email;
                                    TempData["Password"] = model.Password;
                                    TempData["Role"] = role;

<<<<<<< HEAD
=======
=======
                                    // Redirect based on role
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
                                    if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                                    {
                                        return RedirectToAction("ddryfruitsandspices", "Home");
                                    }
                                    else if (role.Equals("user", StringComparison.OrdinalIgnoreCase))
                                    {
<<<<<<< HEAD
                                        return RedirectToAction("Index", "Combined");
=======
                                        return RedirectToAction("adminpage", "Home");
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "Role is not recognized.");
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Invalid email or password.");
                                }
                            }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while processing your request.");
                }
            }

            return View(model);
        }

        // GET: ForgotPassword
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: ForgotPassword
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                    {
                        string query = "SELECT Email FROM UserRegistrationDB WHERE Email = @Email";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Email", model.Email);

                            connection.Open();
                            var result = command.ExecuteScalar();

                            if (result != null)
                            {
                                string resetToken = Guid.NewGuid().ToString();
                                string updateQuery = "UPDATE UserRegistrationDB SET ResetToken = @ResetToken, TokenExpiry = @TokenExpiry WHERE Email = @Email";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@ResetToken", resetToken);
                                    updateCommand.Parameters.AddWithValue("@TokenExpiry", DateTime.UtcNow.AddHours(1));
                                    updateCommand.Parameters.AddWithValue("@Email", model.Email);
                                    updateCommand.ExecuteNonQuery();
                                }

                                string resetLink = Url.Action("ResetPassword", "Login", new { token = resetToken }, Request.Url.Scheme);
                                SendResetEmail(model.Email, resetLink);

                                ViewBag.Message = "Password reset link has been sent to your email.";
                                return View();
                            }
                            else
                            {
                                ModelState.AddModelError("", "Email address not found.");
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
                            else
                            {
                                ModelState.AddModelError("", "Invalid email or password.");
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                            }
                        }
                    }
                }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while processing your request.");
                }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
            }

            return View(model);
        }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b

        private void SendResetEmail(string email, string resetLink)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential("tejalavu96@gmail.com", "tmguzuomjonxoxtb"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("no-reply@yourdomain.com"),
                Subject = "Password Reset Request",
                Body = $"Please reset your password by clicking <a href='{resetLink}'>here</a>.",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Email sending failed: {ex.Message}");
                throw;
            }
        }

        // GET: ResetPassword
        [HttpGet]
        public ActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Token = token;
            return View();
        }

        // POST: ResetPassword
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                    {
                        string query = "SELECT Email FROM UserRegistrationDB WHERE ResetToken = @ResetToken AND TokenExpiry > @CurrentDate";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ResetToken", ViewBag.Token);
                            command.Parameters.AddWithValue("@CurrentDate", DateTime.UtcNow);

                            connection.Open();
                            var result = command.ExecuteScalar();

                            if (result != null)
                            {
                                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                                string updateQuery = "UPDATE UserRegistrationDB SET Password = @Password, ResetToken = NULL, TokenExpiry = NULL WHERE ResetToken = @ResetToken";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@Password", hashedPassword);
                                    updateCommand.Parameters.AddWithValue("@ResetToken", ViewBag.Token);
                                    updateCommand.ExecuteNonQuery();
                                }

                                ViewBag.Message = "Your password has been reset successfully.";
                                return RedirectToAction("Login");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Invalid or expired reset token.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while processing your request.");
                }
            }

            return View(model);
        }

        // GET: EditUser
        [HttpGet]
        public ActionResult EditUser()
        {
            var email = Session["Email"] as string;

            if (string.IsNullOrEmpty(email))
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1

        [HttpGet]
        public ActionResult EditUser()
        {
            var email = TempData["Email"] as string;

            if (email == null)
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
            {
                return RedirectToAction("Login");
            }

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    string query = "SELECT Email, Password, Role, Gender, FirstName, LastName FROM UserRegistrationDB WHERE Email = @Email";
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
                                    OldPassword = reader["Password"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString()
                                };

                                return View(editUserModel);
                            }
                            else
                            {
                                return RedirectToAction("Login");
                            }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                        }
                    }
                }
            }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                return RedirectToAction("Login");
            }
        }

        // POST: EditUser
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======

            return RedirectToAction("Login");
        }

>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
        [HttpPost]
        public ActionResult EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                try
                {
                    using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                    {
                        string query = "UPDATE UserRegistrationDB SET Email = @Email, Password = @Password, Role = @Role, Gender = @Gender, FirstName = @FirstName, LastName = @LastName WHERE Email = @OldEmail";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Email", model.Email);
                            command.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(model.NewPassword));
                            command.Parameters.AddWithValue("@Role", model.Role);
                            command.Parameters.AddWithValue("@Gender", model.Gender);
                            command.Parameters.AddWithValue("@FirstName", model.FirstName);
                            command.Parameters.AddWithValue("@LastName", model.LastName);
                            command.Parameters.AddWithValue("@OldEmail", Session["Email"].ToString());

                            connection.Open();
                            command.ExecuteNonQuery();

                            Session["Email"] = model.Email;
                            Session["Role"] = model.Role;
                            Session["FirstName"] = model.FirstName;
                            Session["LastName"] = model.LastName;

                            return RedirectToAction("EditUser");
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while updating your details.");
                }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
            }

            return View(model);
        }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
    }
}

using System;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Mvc;
using WebApplication8.Models;
using BCrypt.Net;
using System.Configuration;

namespace WebApplication8.Controllers
{
    public class LoginController : Controller
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
        }

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
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
            {
                return RedirectToAction("Login");
            }

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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                return RedirectToAction("Login");
            }
        }

        // POST: EditUser
        [HttpPost]
        public ActionResult EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
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
            }

            return View(model);
        }
    }
}

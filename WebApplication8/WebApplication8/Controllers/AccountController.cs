<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
﻿using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using WebApplication8.Models;

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
﻿// Controllers/AccountController.cs
using WebApplication8.Models;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
namespace WebApplication8.Controllers
{
    public class AccountController : Controller
    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
        private static string randomCode;
        private static DateTime otpGenerationTime;
        public static string to;

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Generate OTP before saving to the database
                    Random rand = new Random();
                    randomCode = rand.Next(100000, 999999).ToString("D6"); // Ensures a 6-digit OTP
                    otpGenerationTime = DateTime.Now;
                    user.OTP = randomCode;

                    // Hash the password (simple example, use a better hashing mechanism in production)
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                    // Save user to the database
                    string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO UserRegistrationDB (FirstName, LastName, Email, Password, DateOfBirth, Gender, Address, Pincode, OTP, Role) " +
                                       "VALUES (@FirstName, @LastName, @Email, @Password, @DateOfBirth, @Gender, @Address, @Pincode, @OTP, @Role)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", user.FirstName);
                            command.Parameters.AddWithValue("@LastName", user.LastName);
                            command.Parameters.AddWithValue("@Email", user.Email);
                            command.Parameters.AddWithValue("@Password", user.Password);
                            command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                            command.Parameters.AddWithValue("@Gender", user.Gender);
                            command.Parameters.AddWithValue("@Address", user.Address);
                            command.Parameters.AddWithValue("@Pincode", user.Pincode);
                            command.Parameters.AddWithValue("@OTP", user.OTP);
                            command.Parameters.AddWithValue("@Role", user.Role);

                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }

                    // Send OTP email
                    bool emailSent = SendOTPEmail(user.Email, randomCode);
                    if (emailSent)
                    {
                        // Redirect to OTP verification page
                        TempData["Email"] = user.Email;
                        return RedirectToAction("VerifyOTP");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error sending OTP email. Please check your email configuration.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error: " + ex.Message);
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult VerifyOTP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerifyOTP(string otp)
        {
            string email = TempData["Email"] as string;
            if (otp == randomCode && (DateTime.Now - otpGenerationTime).TotalMinutes < 15)
            {
                // OTP is correct and within the valid time frame
                // Activate user or perform further actions
                TempData["Message"] = "OTP verified successfully!";
                return RedirectToAction("Login");
            }
            else
            {
                // OTP is incorrect or expired
                ModelState.AddModelError("", "Invalid or expired OTP. Please try again.");
            }
            return View();
        }

        private bool SendOTPEmail(string toEmail, string otp)
        {
            try
            {
                string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
                string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
                string smtpUser = ConfigurationManager.AppSettings["SMTPUser"];
                string smtpPass = ConfigurationManager.AppSettings["SMTPPass"];

                SmtpClient smtpClient = new SmtpClient(smtpServer)
                {
                    Port = int.Parse(smtpPort),
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = true,
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(smtpUser),
                    Subject = "Your OTP Code",
                    Body = $"Your OTP code is {otp}",
                };
                mail.To.Add(toEmail);

                smtpClient.Send(mail);
                return true;
            }
            catch (SmtpException smtpEx)
            {
                ModelState.AddModelError("", "SMTP Error: " + smtpEx.Message);
                System.Diagnostics.Debug.WriteLine("SMTP Error: " + smtpEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error sending email: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("Error sending email: " + ex.Message);
                return false;
            }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======

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
>>>>>>> 8ba5cf9f26c3da9b84a089ecd20bdeb7ccfa61f1
>>>>>>> 54d77b7c45c4b7ef1f01ba38718b00b0a2655a7e
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
        }
    }
}

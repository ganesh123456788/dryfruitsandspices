using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class AccountController : Controller
    {
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
        }
    }
}

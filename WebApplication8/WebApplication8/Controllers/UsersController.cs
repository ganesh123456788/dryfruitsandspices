using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.SqlClient;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class UsersController : Controller
    {
        private readonly string connectionString = "Data Source=DESKTOP-5M6SBGL;Initial Catalog=webapps;Integrated Security=True";

        // GET: Users
        public ActionResult Index(string roleFilter = null, string firstNameFilter = null, string lastNameFilter = null, string emailFilter = null)
        {
            List<Users> users = new List<Users>();
            List<string> roles = new List<string>();
            List<string> firstNames = new List<string>();
            List<string> lastNames = new List<string>();
            List<string> emails = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Get distinct roles for the dropdown
                    string rolesQuery = "SELECT DISTINCT Role FROM UserRegistrationDB";
                    using (SqlCommand rolesCmd = new SqlCommand(rolesQuery, conn))
                    {
                        using (SqlDataReader rolesReader = rolesCmd.ExecuteReader())
                        {
                            while (rolesReader.Read())
                            {
                                roles.Add(rolesReader["Role"].ToString());
                            }
                        }
                    }

                    // Get distinct first names for the dropdown
                    string firstNamesQuery = "SELECT DISTINCT FirstName FROM UserRegistrationDB";
                    using (SqlCommand firstNamesCmd = new SqlCommand(firstNamesQuery, conn))
                    {
                        using (SqlDataReader firstNamesReader = firstNamesCmd.ExecuteReader())
                        {
                            while (firstNamesReader.Read())
                            {
                                firstNames.Add(firstNamesReader["FirstName"].ToString());
                            }
                        }
                    }

                    // Get distinct last names for the dropdown
                    string lastNamesQuery = "SELECT DISTINCT LastName FROM UserRegistrationDB";
                    using (SqlCommand lastNamesCmd = new SqlCommand(lastNamesQuery, conn))
                    {
                        using (SqlDataReader lastNamesReader = lastNamesCmd.ExecuteReader())
                        {
                            while (lastNamesReader.Read())
                            {
                                lastNames.Add(lastNamesReader["LastName"].ToString());
                            }
                        }
                    }

                    // Get distinct emails for the dropdown
                    string emailsQuery = "SELECT DISTINCT Email FROM UserRegistrationDB";
                    using (SqlCommand emailsCmd = new SqlCommand(emailsQuery, conn))
                    {
                        using (SqlDataReader emailsReader = emailsCmd.ExecuteReader())
                        {
                            while (emailsReader.Read())
                            {
                                emails.Add(emailsReader["Email"].ToString());
                            }
                        }
                    }

                    // SQL query with optional filters
                    string query = "SELECT FirstName, LastName, Email, Role FROM UserRegistrationDB WHERE 1=1";

                    if (!string.IsNullOrEmpty(roleFilter))
                    {
                        query += " AND Role = @Role";
                    }
                    if (!string.IsNullOrEmpty(firstNameFilter))
                    {
                        query += " AND FirstName = @FirstName";
                    }
                    if (!string.IsNullOrEmpty(lastNameFilter))
                    {
                        query += " AND LastName = @LastName";
                    }
                    if (!string.IsNullOrEmpty(emailFilter))
                    {
                        query += " AND Email = @Email";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(roleFilter))
                        {
                            cmd.Parameters.AddWithValue("@Role", roleFilter);
                        }
                        if (!string.IsNullOrEmpty(firstNameFilter))
                        {
                            cmd.Parameters.AddWithValue("@FirstName", firstNameFilter);
                        }
                        if (!string.IsNullOrEmpty(lastNameFilter))
                        {
                            cmd.Parameters.AddWithValue("@LastName", lastNameFilter);
                        }
                        if (!string.IsNullOrEmpty(emailFilter))
                        {
                            cmd.Parameters.AddWithValue("@Email", emailFilter);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Users user = new Users
                                {
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Role = reader["Role"].ToString()
                                };
                                users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching data from the database.", ex);
            }

            // Pass the current filter values and dropdown lists to the view
            ViewBag.RoleFilter = roleFilter;
            ViewBag.FirstNameFilter = firstNameFilter;
            ViewBag.LastNameFilter = lastNameFilter;
            ViewBag.EmailFilter = emailFilter;
            ViewBag.Roles = new SelectList(roles);
            ViewBag.FirstNames = new SelectList(firstNames);
            ViewBag.LastNames = new SelectList(lastNames);
            ViewBag.Emails = new SelectList(emails);

            return View(users);
        }
    }
}

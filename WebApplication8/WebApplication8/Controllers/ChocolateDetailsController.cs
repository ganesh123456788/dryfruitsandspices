using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class ChocolateDetailsController : Controller
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: ChocolateDetails/Details1/{id}
        public ActionResult Details1(string id)
        {
            // Check if the id is null or empty
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound(); // Return a 404 if the id is null or empty
            }

            Chocolate chocolate = null; // Initialize as null to handle case where no data is found

            // Use a using statement for SqlConnection
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sqlQuery = "SELECT ImageName, ImagePath, Description, Price FROM Chocolate WHERE ImageName = @ImageName";
                // Use a using statement for SqlCommand
                using (var command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ImageName", id);
                    connection.Open();

                    // Use a using statement for SqlDataReader
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate the Chocolate object from the database record
                            chocolate = new Chocolate
                            {
                                ImageName = reader["ImageName"].ToString(),
                                ImagePath = reader["ImagePath"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]) // Keep Price as decimal for accuracy
                            };
                        }
                    }
                }
            }

            // Check if no chocolate was found and return 404
            if (chocolate == null)
            {
                return HttpNotFound(); // Return a 404 if no chocolate found
            }

            return View(chocolate); // Return the view with the chocolate details
        }
    }
}

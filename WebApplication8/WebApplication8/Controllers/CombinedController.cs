using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class CombinedController : Controller
    {
        private string connectionString = "Data Source=DESKTOP-5M6SBGL;Initial Catalog=webapps;Integrated Security=True";

        public class ProductDetailViewModel
        {
            public string ImageName { get; set; }
            public string ImagePath { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string ProductType { get; set; } // "DryFruits", "Spices", or "Chocolates"
            public string ShortStory { get; set; } // New property
        }

        // Action to display product list on the Index page
        public ActionResult Index()
        {
            var model = new List<ProductDetailViewModel>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT ImageName, ImagePath, Description, Price, ShortStory, 'DryFruits' AS ProductType FROM DryFruits
                        UNION ALL
                        SELECT ImageName, ImagePath, Description, Price, ShortStory, 'Spices' AS ProductType FROM Spices
                        UNION ALL
                        SELECT ImageName, ImagePath, Description, Price, ShortStory, 'Chocolate' AS ProductType FROM Chocolate";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.Add(new ProductDetailViewModel
                                {
                                    ImageName = reader["ImageName"].ToString(),
                                    ImagePath = reader["ImagePath"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    ProductType = reader["ProductType"].ToString(),
                                    ShortStory = reader["ShortStory"].ToString() // New property
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (uncomment ex variable name and write a log)
                // Console.WriteLine(ex.Message);
                // Optionally, set an error message to display in the view
                ViewBag.ErrorMessage = "An error occurred while retrieving data.";
            }

            return View(model);
        }
    }
}

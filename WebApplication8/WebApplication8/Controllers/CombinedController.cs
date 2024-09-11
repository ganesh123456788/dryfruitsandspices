using System;
using System.Collections.Generic;
using System.Data.SqlClient;
<<<<<<< HEAD
using System.Linq;
using System.Web.Mvc;
=======
using System.Web.Mvc;
using WebApplication8.Models;
>>>>>>> 901688282898ff11154d4a648ba17e842570c831

namespace WebApplication8.Controllers
{
    public class CombinedController : Controller
    {
<<<<<<< HEAD
        private readonly string connectionString = "Data Source=DESKTOP-5M6SBGL;Initial Catalog=webapps;Integrated Security=True";

        // ViewModel for product details
=======
        private string connectionString = "Data Source=DESKTOP-5M6SBGL;Initial Catalog=webapps;Integrated Security=True";

>>>>>>> 901688282898ff11154d4a648ba17e842570c831
        public class ProductDetailViewModel
        {
            public string ImageName { get; set; }
            public string ImagePath { get; set; }
            public string Description { get; set; }
<<<<<<< HEAD
            public string ShortStory { get; set; }
            public decimal Price { get; set; }
            public string ProductType { get; set; } // "DryFruits", "Spices", or "Chocolate"
        }

        // Action to display product list on the Index page
        public ActionResult Index(string query, string filter, string sort)
=======
            public decimal Price { get; set; }
            public string ProductType { get; set; } // "DryFruits", "Spices", or "Chocolates"
            public string ShortStory { get; set; } // New property
        }

        // Action to display product list on the Index page
        public ActionResult Index()
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
        {
            var model = new List<ProductDetailViewModel>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

<<<<<<< HEAD
                    // SQL query to fetch product details from multiple tables
                    string sqlQuery = @"
=======
                    string query = @"
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
                        SELECT ImageName, ImagePath, Description, Price, ShortStory, 'DryFruits' AS ProductType FROM DryFruits
                        UNION ALL
                        SELECT ImageName, ImagePath, Description, Price, ShortStory, 'Spices' AS ProductType FROM Spices
                        UNION ALL
                        SELECT ImageName, ImagePath, Description, Price, ShortStory, 'Chocolate' AS ProductType FROM Chocolate";

<<<<<<< HEAD
                    using (var command = new SqlCommand(sqlQuery, connection))
=======
                    using (var command = new SqlCommand(query, connection))
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
<<<<<<< HEAD
                                var price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;

=======
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
                                model.Add(new ProductDetailViewModel
                                {
                                    ImageName = reader["ImageName"].ToString(),
                                    ImagePath = reader["ImagePath"].ToString(),
                                    Description = reader["Description"].ToString(),
<<<<<<< HEAD
                                    ShortStory = reader["ShortStory"].ToString(),
                                    Price = price,
                                    ProductType = reader["ProductType"].ToString()
=======
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    ProductType = reader["ProductType"].ToString(),
                                    ShortStory = reader["ShortStory"].ToString() // New property
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                ViewBag.ErrorMessage = "An error occurred while retrieving product data. Please try again later.";
                // Log the exception (e.g., to a file or logging service)
                // Optionally, log the exception details: 
                // System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            // Filtering based on query and filter parameters
            if (!string.IsNullOrEmpty(query))
            {
                model = model.Where(p => p.ImageName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         p.Description.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            if (!string.IsNullOrEmpty(filter))
            {
                model = model.Where(p => p.ProductType.Equals(filter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Sorting by price
            if (sort == "asc")
            {
                model = model.OrderBy(p => p.Price).ToList();
            }
            else if (sort == "desc")
            {
                model = model.OrderByDescending(p => p.Price).ToList();
            }

            ViewBag.SearchQuery = query;
            ViewBag.Filter = filter;
            ViewBag.Sort = sort;

=======
                // Log the error (uncomment ex variable name and write a log)
                // Console.WriteLine(ex.Message);
                // Optionally, set an error message to display in the view
                ViewBag.ErrorMessage = "An error occurred while retrieving data.";
            }

>>>>>>> 901688282898ff11154d4a648ba17e842570c831
            return View(model);
        }
    }
}

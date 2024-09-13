using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication8.Controllers
{
    public class CombinedController : Controller
    {
        private readonly string connectionString = "Data Source=DESKTOP-5M6SBGL;Initial Catalog=webapps;Integrated Security=True";

        // ViewModel for product details
        public class ProductDetailViewModel
        {
            public string ImageName { get; set; }
            public string ImagePath { get; set; }
            public string Description { get; set; }
            public string ShortStory { get; set; }
            public decimal Price { get; set; }
            public string ProductType { get; set; } // "DryFruits", "Spices", or "Chocolate"
        }

        // Action to display product list on the Index page
        public ActionResult Index(string query, string filter, string sort)
        {
            var model = new List<ProductDetailViewModel>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to fetch product details from multiple tables
                    string sqlQuery = @"
                        SELECT ImageName, ImagePath, Description, Price, ShortStory, 'DryFruits' AS ProductType FROM DryFruits
                        UNION ALL
                        SELECT ImageName, ImagePath, Description, Price, ShortStory, 'Spices' AS ProductType FROM Spices
                        UNION ALL
                        SELECT ImageName, ImagePath, Description, Price, ShortStory, 'Chocolate' AS ProductType FROM Chocolate";

                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;

                                model.Add(new ProductDetailViewModel
                                {
                                    ImageName = reader["ImageName"].ToString(),
                                    ImagePath = reader["ImagePath"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    ShortStory = reader["ShortStory"].ToString(),
                                    Price = price,
                                    ProductType = reader["ProductType"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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

            return View(model);
        }
    }
}

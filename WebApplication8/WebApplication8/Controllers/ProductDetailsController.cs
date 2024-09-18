using System.Data.SqlClient;
using System.Web.Mvc;

namespace WebApplication8.Controllers
{
    public class ProductDetailsController : Controller
    {
        private string connectionString = "Data Source=DESKTOP-5M6SBGL;Initial Catalog=webapps;Integrated Security=True";

        // Nested model class inside the controller
        public class ProductDetailViewModel
        {
            public string ImageName { get; set; }
            public string ImagePath { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string ItemType { get; set; }
            public string ProductType { get; set; } // "DryFruits", "Spices", or "Chocolates"
            public int SelectedWeight { get; set; } // New property for weight
            public string ShortStory { get; internal set; }
        }

        // Action to get product details
        public ActionResult GetProductDetails(string imageName, string productType, int selectedWeight = 250)
        {
            var productDetails = new ProductDetailViewModel { SelectedWeight = selectedWeight };

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "";

                if (productType == "Spices")
                {
                    query = "SELECT ImageName, ImagePath, Description, Price FROM Spices WHERE ImageName = @ImageName";
                }
                else if (productType == "DryFruits")
                {
                    query = "SELECT ImageName, ImagePath, Description, Price FROM DryFruits WHERE ImageName = @ImageName";
                }
                else if (productType == "Chocolate")
                {
                    query = "SELECT ImageName, ImagePath, Description, Price FROM Chocolate WHERE ImageName = @ImageName";
                }

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ImageName", imageName);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal basePrice = (decimal)reader["Price"];

                            // Adjust the price based on the selected weight
                            productDetails = new ProductDetailViewModel
                            {
                                ImageName = reader["ImageName"].ToString(),
                                ImagePath = reader["ImagePath"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = basePrice * selectedWeight / 250, // Assume 250gms is the base weight
                                ProductType = productType,
                                SelectedWeight = selectedWeight
                            };
                        }
                    }
                }
            }

            return View("ProductDetails", productDetails); // Ensure this matches the view file name
        }
    }
}

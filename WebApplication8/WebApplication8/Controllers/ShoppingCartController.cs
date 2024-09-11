using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebApplication8.Models;

namespace ShoppingCartApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        private const decimal GSTPercentage = 0.18m; // 18% GST
        private const decimal DeliveryCharge = 50.00m; // Fixed delivery charge

        public ActionResult Index()
        {
            var cart = GetCart();
            foreach (var item in cart)
            {
                item.GST = item.Price * GSTPercentage;
                item.TotalPrice = (item.Price + item.GST) * item.Quantity;
                item.TotalPrice += DeliveryCharge; // Add delivery charges for each item
            }
            return View(cart);
        }

        public ActionResult AddToCart(string imageName, string itemType, int quantity = 1)
        {
            dynamic item = GetItemByImageName(imageName, itemType);
            if (item != null)
            {
                var cart = GetCart();
                var cartItem = cart.FirstOrDefault(i => i.ProductName == item.ImageName);
                if (cartItem == null)
                {
                    cart.Add(new CartItem
                    {
                        ProductName = item.ImageName,
                        Price = item.Price,
                        Quantity = quantity,
                        ImagePath = item.ImagePath,
                        Description = item.Description,
                        GST = item.Price * GSTPercentage, // Calculate GST
                        DeliveryCharge = DeliveryCharge, // Set delivery charge
                        TotalPrice = (item.Price + (item.Price * GSTPercentage)) * quantity + DeliveryCharge // Calculate total price
                    });
                }
                else
                {
                    cartItem.Quantity += quantity;
                    cartItem.TotalPrice = (cartItem.Price + cartItem.GST) * cartItem.Quantity + cartItem.DeliveryCharge;
                }
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            // Assuming you store the cart items in a session
            var cart = Session["Cart"] as List<CartItem>;

            if (cart != null)
            {
                var itemToRemove = cart.FirstOrDefault(i => i.Price == productId);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                }

                // Update the session
                Session["Cart"] = cart;
            }

            // You can return a simple JSON result indicating success
            return Json(new { success = true });
        }


        private List<CartItem> GetCart()
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }

            return cart;
        }

        private void SaveCart(List<CartItem> cart)
        {
            Session["Cart"] = cart;
        }

        private dynamic GetItemByImageName(string imageName, string itemType)
        {
            dynamic item = null;
            string query = string.Empty;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (itemType == "DryFruits")
                {
                    query = "SELECT ImageName, ImagePath, Description, Price FROM DryFruits WHERE ImageName = @ImageName";
                }
                else if (itemType == "Spices")
                {
                    query = "SELECT ImageName, ImagePath, Description, Price FROM Spices WHERE ImageName = @ImageName";
                }
                else if (itemType == "Chocolate")
                {
                    query = "SELECT ImageName, ImagePath, Description, Price FROM Chocolate WHERE ImageName = @ImageName";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ImageName", imageName);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    item = new
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"])
                    };
                }
            }

            return item;
        }

        public class CartItem
        {
            public string ProductName { get; set; }
            public string ImagePath { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal GST { get; set; } // GST on the product
            public decimal DeliveryCharge { get; set; } // Delivery charges
            public decimal TotalPrice { get; set; } // Total price including GST and delivery charges
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class StockController : Controller
    {
        private readonly string _connectionString = "Data Source=DESKTOP-5M6SBGL;Initial Catalog=webapps;Integrated Security=True";

        public ActionResult Index()
        {
            var viewModel = new CombinedViewModel
            {
                SpicesList = GetData<Spices>("SELECT ImageName, ImagePath, Description, Price FROM Spices"),
                DryFruitsList = GetData<DryFruits>("SELECT ImageName, ImagePath, Description, Price FROM DryFruits"),
                ChocolatesList = GetData<Chocolate>("SELECT ImageName, ImagePath, Description, Price FROM Chocolate")
            };

            return View(viewModel);
        }

        private List<T> GetData<T>(string query) where T : new()
        {
            var list = new List<T>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new T();
                        var properties = typeof(T).GetProperties();
                        for (int i = 0; i < properties.Length; i++)
                        {
                            var property = properties[i];
                            // Skip HttpPostedFileBase properties
                            if (property.PropertyType == typeof(HttpPostedFileBase))
                                continue;

                            // Get the value from the reader by column name
                            var columnName = property.Name;
                            var value = reader[columnName];
                            if (value != DBNull.Value)
                            {
                                property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                            }
                        }
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string category, string description = "Default Description", int price = 0)
        {
            if (file != null && file.ContentLength > 0 && IsValidCategory(category))
            {
                var fileName = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("~/Content/"), fileName); // Adjusted to save in the Stock directory
                file.SaveAs(path);

                string query = GetInsertQuery(category);

                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageName", fileName);
                    command.Parameters.AddWithValue("@ImagePath", "~/Content/" + fileName); // Adjusted path to match URL
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Price", price);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                // Handle invalid file or category
                ModelState.AddModelError("", "Invalid file or category.");
            }

            return RedirectToAction("Index");
        }

        private bool IsValidCategory(string category)
        {
            return category == "Spices" || category == "DryFruits" || category == "Chocolates";
        }

        private string GetInsertQuery(string category)
        {
            switch (category)
            {
                case "Spices":
                    return "INSERT INTO Spices (ImageName, ImagePath, Description, Price) VALUES (@ImageName, @ImagePath, @Description, @Price)";
                case "DryFruits":
                    return "INSERT INTO DryFruits (ImageName, ImagePath, Description, Price) VALUES (@ImageName, @ImagePath, @Description, @Price)";
                case "Chocolates":
                    return "INSERT INTO Chocolate (ImageName, ImagePath, Description, Price) VALUES (@ImageName, @ImagePath, @Description, @Price)";
                default:
                    throw new ArgumentException("Invalid category");
            }
        }
    }
}

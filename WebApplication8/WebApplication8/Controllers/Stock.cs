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
<<<<<<< HEAD
                SpicesList = GetData<Spices>("SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices"),
                DryFruitsList = GetData<DryFruits>("SELECT ImageName, ImagePath, Description, ShortStory, Price FROM DryFruits"),
                ChocolatesList = GetData<Chocolate>("SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate")
=======
<<<<<<< HEAD
                SpicesList = GetData<Spices>("SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices"),
                DryFruitsList = GetData<DryFruits>("SELECT ImageName, ImagePath, Description, ShortStory, Price FROM DryFruits"),
                ChocolatesList = GetData<Chocolate>("SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate")
=======
<<<<<<< HEAD
                SpicesList = GetData<Spices>("SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices"),
                DryFruitsList = GetData<DryFruits>("SELECT ImageName, ImagePath, Description, ShortStory, Price FROM DryFruits"),
                ChocolatesList = GetData<Chocolate>("SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate")
=======
                SpicesList = GetData<Spices>("SELECT ImageName, ImagePath, Description, Price FROM Spices"),
                DryFruitsList = GetData<DryFruits>("SELECT ImageName, ImagePath, Description, Price FROM DryFruits"),
                ChocolatesList = GetData<Chocolate>("SELECT ImageName, ImagePath, Description, Price FROM Chocolate")
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                            if (value != DBNull.Value)
                            {
                                property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                            }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
                            property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                        }
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        [HttpPost]
<<<<<<< HEAD
        public ActionResult Upload(HttpPostedFileBase file, string category, string description = "Default Description", string ShortStory = "Default Description", int price = 0)
=======
<<<<<<< HEAD
        public ActionResult Upload(HttpPostedFileBase file, string category, string description = "Default Description", string ShortStory = "Default Description", int price = 0)
=======
<<<<<<< HEAD
        public ActionResult Upload(HttpPostedFileBase file, string category, string description = "Default Description", string ShortStory = "Default Description", int price = 0)
=======
<<<<<<< HEAD
        public ActionResult Upload(HttpPostedFileBase file, string category, string description = "Default Description", int price = 0)
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
        {
            if (file != null && file.ContentLength > 0 && IsValidCategory(category))
            {
                var fileName = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("~/Content/"), fileName); // Adjusted to save in the Stock directory
                file.SaveAs(path);

                string query = GetInsertQuery(category);
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
        public ActionResult Upload(HttpPostedFileBase file, string category)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("~/Content/"), fileName);
                file.SaveAs(path);

                string query = string.Empty;
                switch (category)
                {
                    case "Spices":
                        query = "INSERT INTO Spices (ImageName, ImagePath, Description, Price) VALUES (@ImageName, @ImagePath, @Description, @Price)";
                        break;
                    case "DryFruits":
                        query = "INSERT INTO DryFruits (ImageName, ImagePath, Description, Price) VALUES (@ImageName, @ImagePath, @Description, @Price)";
                        break;
                    case "Chocolates":
                        query = "INSERT INTO Chocolate (ImageName, ImagePath, Description, Price) VALUES (@ImageName, @ImagePath, @Description, @Price)";
                        break;
                }
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b

                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageName", fileName);
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                    command.Parameters.AddWithValue("@ImagePath", "~/Content/" + fileName); // Adjusted path to match URL
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@ShortStory", ShortStory);
                    command.Parameters.AddWithValue("@Price", price);
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
<<<<<<< HEAD
                    command.Parameters.AddWithValue("@ImagePath", "~/Content/" + fileName); // Adjusted path to match URL
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Price", price);
=======
                    command.Parameters.AddWithValue("@ImagePath", "/Content/" + fileName); // Ensure this path is correct
                    command.Parameters.AddWithValue("@Description", "Default Description");
                    command.Parameters.AddWithValue("@Price", 0);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                    return "INSERT INTO Spices (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                case "DryFruits":
                    return "INSERT INTO DryFruits (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                case "Chocolates":
                    return "INSERT INTO Chocolate (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
                    return "INSERT INTO Spices (ImageName, ImagePath, Description, Price) VALUES (@ImageName, @ImagePath, @Description, @Price)";
                case "DryFruits":
                    return "INSERT INTO DryFruits (ImageName, ImagePath, Description, Price) VALUES (@ImageName, @ImagePath, @Description, @Price)";
                case "Chocolates":
                    return "INSERT INTO Chocolate (ImageName, ImagePath, Description, Price) VALUES (@ImageName, @ImagePath, @Description, @Price)";
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                default:
                    throw new ArgumentException("Invalid category");
            }
        }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======

            return RedirectToAction("Index");
        }
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
    }
}

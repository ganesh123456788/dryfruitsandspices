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
                            property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                        }
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        [HttpPost]
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

                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageName", fileName);
                    command.Parameters.AddWithValue("@ImagePath", "/Content/" + fileName); // Ensure this path is correct
                    command.Parameters.AddWithValue("@Description", "Default Description");
                    command.Parameters.AddWithValue("@Price", 0);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
    }
}

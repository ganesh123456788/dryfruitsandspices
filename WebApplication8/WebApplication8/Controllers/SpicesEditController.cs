using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class SpicesEditController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: SpicesEdit/Index
        public ActionResult Index()
        {
            var spicesList = new List<Spices>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var spices = new Spices
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty, // Handle DBNull for ImagePath
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty, // Handle DBNull for Description
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0, // Handle DBNull for Price
                        ShortStory = reader["ShortStory"] != DBNull.Value ? reader["ShortStory"].ToString() : string.Empty // Handle DBNull for ShortStory
                    };
                    spicesList.Add(spices);
                }
                connection.Close();
            }
            return View(spicesList);
        }

        // GET: SpicesEdit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpicesEdit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Spices spices)
        {
            if (ModelState.IsValid)
            {
                string imagePath = null;
                if (spices.ImageFile != null)
                {
                    string fileName = Path.GetFileName(spices.ImageFile.FileName);
                    imagePath = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    spices.ImageFile.SaveAs(imagePath);
                    spices.ImagePath = "/Content/" + fileName; // Correctly store the full path
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Spices (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", spices.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", spices.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", spices.Description);
                    command.Parameters.AddWithValue("@Price", spices.Price);
                    command.Parameters.AddWithValue("@ShortStory", spices.ShortStory ?? (object)DBNull.Value); // Added ShortStory parameter

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
            return View(spices);
        }

        // GET: SpicesEdit/Edit
        public ActionResult Edit(string imageName)
        {
            Spices spices = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spices = new Spices
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty, // Handle DBNull for ImagePath
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty, // Handle DBNull for Description
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0, // Handle DBNull for Price
                        ShortStory = reader["ShortStory"] != DBNull.Value ? reader["ShortStory"].ToString() : string.Empty // Handle DBNull for ShortStory
                    };
                }
                connection.Close();
            }
            return View(spices);
        }

        // POST: SpicesEdit/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Spices spices)
        {
            if (ModelState.IsValid)
            {
                string imagePath = spices.ImagePath;
                if (spices.ImageFile != null)
                {
                    string fileName = Path.GetFileName(spices.ImageFile.FileName);
                    imagePath = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    spices.ImageFile.SaveAs(imagePath);
                    spices.ImagePath = "/Content/" + fileName; // Correctly store the full path
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Spices SET ImagePath = @ImagePath, Description = @Description, Price = @Price, ShortStory = @ShortStory WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", spices.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", spices.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", spices.Description);
                    command.Parameters.AddWithValue("@Price", spices.Price);
                    command.Parameters.AddWithValue("@ShortStory", spices.ShortStory ?? (object)DBNull.Value); // Added ShortStory parameter

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
            return View(spices);
        }

        // GET: SpicesEdit/Delete
        public ActionResult Delete(string imageName)
        {
            Spices spices = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    spices = new Spices
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty, // Handle DBNull for ImagePath
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty, // Handle DBNull for Description
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0, // Handle DBNull for Price
                        ShortStory = reader["ShortStory"] != DBNull.Value ? reader["ShortStory"].ToString() : string.Empty // Handle DBNull for ShortStory
                    };
                }
                connection.Close();
            }
            return View(spices);
        }

        // POST: SpicesEdit/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string imageName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Spices WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}

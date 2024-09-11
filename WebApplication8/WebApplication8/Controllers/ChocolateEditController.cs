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
    public class ChocolateEditController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: ChocolateEdit/Index
        public ActionResult Index()
        {
            var chocolatesList = new List<Chocolate>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var chocolate = new Chocolate
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty,
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0,
                        ShortStory = reader["ShortStory"] != DBNull.Value ? reader["ShortStory"].ToString() : string.Empty
                    };
                    chocolatesList.Add(chocolate);
                }
                connection.Close();
            }
            return View(chocolatesList);
        }

        // GET: ChocolateEdit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChocolateEdit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Chocolate chocolate)
        {
            if (ModelState.IsValid)
            {
                string imagePath = null;
                if (chocolate.ImageFile != null)
                {
                    string fileName = Path.GetFileName(chocolate.ImageFile.FileName);
                    imagePath = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    chocolate.ImageFile.SaveAs(imagePath);
                    chocolate.ImagePath = "/Content/" + fileName;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Chocolate (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", chocolate.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", chocolate.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", chocolate.Description);
                    command.Parameters.AddWithValue("@Price", chocolate.Price);
                    command.Parameters.AddWithValue("@ShortStory", chocolate.ShortStory ?? (object)DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
            return View(chocolate);
        }

        // GET: ChocolateEdit/Edit
        public ActionResult Edit(string imageName)
        {
            Chocolate chocolate = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    chocolate = new Chocolate
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty,
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0,
                        ShortStory = reader["ShortStory"] != DBNull.Value ? reader["ShortStory"].ToString() : string.Empty
                    };
                }
                connection.Close();
            }
            return View(chocolate);
        }

        // POST: ChocolateEdit/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Chocolate chocolate)
        {
            if (ModelState.IsValid)
            {
                string imagePath = chocolate.ImagePath;
                if (chocolate.ImageFile != null)
                {
                    string fileName = Path.GetFileName(chocolate.ImageFile.FileName);
                    imagePath = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    chocolate.ImageFile.SaveAs(imagePath);
                    chocolate.ImagePath = "/Content/" + fileName;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Chocolate SET ImagePath = @ImagePath, Description = @Description, Price = @Price, ShortStory = @ShortStory WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", chocolate.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", chocolate.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", chocolate.Description);
                    command.Parameters.AddWithValue("@Price", chocolate.Price);
                    command.Parameters.AddWithValue("@ShortStory", chocolate.ShortStory ?? (object)DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
            return View(chocolate);
        }

        // GET: ChocolateEdit/Delete
        public ActionResult Delete(string imageName)
        {
            Chocolate chocolate = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    chocolate = new Chocolate
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty,
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0,
                        ShortStory = reader["ShortStory"] != DBNull.Value ? reader["ShortStory"].ToString() : string.Empty
                    };
                }
                connection.Close();
            }
            return View(chocolate);
        }

        // POST: ChocolateEdit/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string imageName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Chocolate WHERE ImageName = @ImageName";
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

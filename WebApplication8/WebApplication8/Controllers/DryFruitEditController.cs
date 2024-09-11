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
    public class DryFruitEditController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: DryFruitEdit/Index
        public ActionResult Index()
        {
            var dryFruitsList = new List<DryFruits>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var dryFruits = new DryFruits
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty,
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0,
                        ShortStory = reader["ShortStory"] != DBNull.Value ? reader["ShortStory"].ToString() : string.Empty
                    };
                    dryFruitsList.Add(dryFruits);
                }
                connection.Close();
            }
            return View(dryFruitsList);
        }

        // GET: DryFruitEdit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DryFruitEdit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DryFruits dryFruits)
        {
            if (ModelState.IsValid)
            {
                string imagePath = null;
                if (dryFruits.ImageFile != null)
                {
                    string fileName = Path.GetFileName(dryFruits.ImageFile.FileName);
                    imagePath = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    dryFruits.ImageFile.SaveAs(imagePath);
                    dryFruits.ImagePath = "/Content/" + fileName;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO DryFruits (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);
                    command.Parameters.AddWithValue("@ShortStory", dryFruits.ShortStory ?? (object)DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
            return View(dryFruits);
        }

        // GET: DryFruitEdit/Edit
        public ActionResult Edit(string imageName)
        {
            DryFruits dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dryFruits = new DryFruits
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
            return View(dryFruits);
        }

        // POST: DryFruitEdit/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DryFruits dryFruits)
        {
            if (ModelState.IsValid)
            {
                string imagePath = dryFruits.ImagePath;
                if (dryFruits.ImageFile != null)
                {
                    string fileName = Path.GetFileName(dryFruits.ImageFile.FileName);
                    imagePath = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    dryFruits.ImageFile.SaveAs(imagePath);
                    dryFruits.ImagePath = "/Content/" + fileName;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE DryFruits SET ImagePath = @ImagePath, Description = @Description, Price = @Price, ShortStory = @ShortStory WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);
                    command.Parameters.AddWithValue("@ShortStory", dryFruits.ShortStory ?? (object)DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
            return View(dryFruits);
        }

        // GET: DryFruitEdit/Delete
        public ActionResult Delete(string imageName)
        {
            DryFruits dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dryFruits = new DryFruits
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
            return View(dryFruits);
        }

        // POST: DryFruitEdit/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string imageName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM DryFruits WHERE ImageName = @ImageName";
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

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
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;

        // GET: SpicesEdit/Index
        public ActionResult Index()
        {
            var spicesList = new List<Spices>();
            string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            spicesList.Add(MapSpices(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
                ViewBag.ErrorMessage = "An error occurred while retrieving data.";
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
                string imagePath = SaveImageFile(spices.ImageFile);
                if (imagePath != null)
                {
                    spices.ImagePath = imagePath;
                }

                string query = "INSERT INTO Spices (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";

                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ImageName", spices.ImageName);
                        command.Parameters.AddWithValue("@ImagePath", (object)spices.ImagePath ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description", spices.Description);
                        command.Parameters.AddWithValue("@Price", spices.Price);
                        command.Parameters.AddWithValue("@ShortStory", (object)spices.ShortStory ?? DBNull.Value);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log exception
                    ViewBag.ErrorMessage = "An error occurred while creating the record.";
                }
            }

            return View(spices);
        }

        // GET: SpicesEdit/Edit
        public ActionResult Edit(string imageName)
        {
            Spices spices = null;
            string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices WHERE ImageName = @ImageName";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ImageName", imageName);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            spices = MapSpices(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
                ViewBag.ErrorMessage = "An error occurred while retrieving data.";
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
                string imagePath = SaveImageFile(spices.ImageFile) ?? spices.ImagePath;

                string query = "UPDATE Spices SET ImagePath = @ImagePath, Description = @Description, Price = @Price, ShortStory = @ShortStory WHERE ImageName = @ImageName";

                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ImageName", spices.ImageName);
                        command.Parameters.AddWithValue("@ImagePath", (object)imagePath ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description", spices.Description);
                        command.Parameters.AddWithValue("@Price", spices.Price);
                        command.Parameters.AddWithValue("@ShortStory", (object)spices.ShortStory ?? DBNull.Value);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log exception
                    ViewBag.ErrorMessage = "An error occurred while updating the record.";
                }
            }

            return View(spices);
        }

        // GET: SpicesEdit/Delete
        public ActionResult Delete(string imageName)
        {
            Spices spices = null;
            string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices WHERE ImageName = @ImageName";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ImageName", imageName);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            spices = MapSpices(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
                ViewBag.ErrorMessage = "An error occurred while retrieving data.";
            }

            return View(spices);
        }

        // POST: SpicesEdit/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string imageName)
        {
            string query = "DELETE FROM Spices WHERE ImageName = @ImageName";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ImageName", imageName);
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log exception
                ViewBag.ErrorMessage = "An error occurred while deleting the record.";
            }

            return RedirectToAction("Index");
        }

        private static Spices MapSpices(SqlDataReader reader)
        {
            return new Spices
            {
                ImageName = reader["ImageName"].ToString(),
                ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty,
                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty,
                Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0,
                ShortStory = reader["ShortStory"] != DBNull.Value ? reader["ShortStory"].ToString() : string.Empty
            };
        }

        private string SaveImageFile(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                return null;
            }

            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/"), fileName);
            file.SaveAs(path);
            return "/Content/" + fileName;
        }
    }
}

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
<<<<<<< HEAD
            var spicesList = new List<Spices>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices";
=======
            var dryFruitsList = new List<Spices>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Spices";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
<<<<<<< HEAD
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
=======
                    var dryFruits = new Spices
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"]?.ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToInt32(reader["Price"])
                    };
                    if (reader["ImageFile"] != DBNull.Value)
                    {
                        byte[] imageData = (byte[])reader["ImageFile"];
                        string base64String = Convert.ToBase64String(imageData);
                        dryFruits.ImagePath = "data:image/png;base64," + base64String;
                    }
                    dryFruitsList.Add(dryFruits);
                }
                connection.Close();
            }
            return View(dryFruitsList);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
        }

        // GET: SpicesEdit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpicesEdit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
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
=======
        public ActionResult Create(Spices dryFruits)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (dryFruits.ImageFile != null)
                {
                    using (var binaryReader = new BinaryReader(dryFruits.ImageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(dryFruits.ImageFile.ContentLength);
                    }
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
<<<<<<< HEAD
                    string query = "INSERT INTO Spices (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", spices.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", spices.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", spices.Description);
                    command.Parameters.AddWithValue("@Price", spices.Price);
                    command.Parameters.AddWithValue("@ShortStory", spices.ShortStory ?? (object)DBNull.Value); // Added ShortStory parameter
=======
                    string query = "INSERT INTO Spices (ImageFile, ImageName, ImagePath, Description, Price) VALUES (@ImageFile, @ImageName, @ImagePath, @Description, @Price)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageFile", imageData ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
<<<<<<< HEAD
            return View(spices);
=======
            return View(dryFruits);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
        }

        // GET: SpicesEdit/Edit
        public ActionResult Edit(string imageName)
        {
<<<<<<< HEAD
            Spices spices = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices WHERE ImageName = @ImageName";
=======
            Spices dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Spices WHERE ImageName = @ImageName";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
<<<<<<< HEAD
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
=======
                    dryFruits = new Spices
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"]?.ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToInt32(reader["Price"])
                    };
                    if (reader["ImageFile"] != DBNull.Value)
                    {
                        byte[] imageData = (byte[])reader["ImageFile"];
                        string base64String = Convert.ToBase64String(imageData);
                        dryFruits.ImagePath = "data:image/png;base64," + base64String;
                    }
                }
                connection.Close();
            }
            return View(dryFruits);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
        }

        // POST: SpicesEdit/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
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
=======
        public ActionResult Edit(Spices dryFruits)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (dryFruits.ImageFile != null)
                {
                    using (var binaryReader = new BinaryReader(dryFruits.ImageFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(dryFruits.ImageFile.ContentLength);
                    }
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
<<<<<<< HEAD
                    string query = "UPDATE Spices SET ImagePath = @ImagePath, Description = @Description, Price = @Price, ShortStory = @ShortStory WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", spices.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", spices.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", spices.Description);
                    command.Parameters.AddWithValue("@Price", spices.Price);
                    command.Parameters.AddWithValue("@ShortStory", spices.ShortStory ?? (object)DBNull.Value); // Added ShortStory parameter
=======
                    string query = "UPDATE Spices SET ImageFile = @ImageFile, ImagePath = @ImagePath, Description = @Description, Price = @Price WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageFile", imageData ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
<<<<<<< HEAD
            return View(spices);
=======
            return View(dryFruits);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
        }

        // GET: SpicesEdit/Delete
        public ActionResult Delete(string imageName)
        {
<<<<<<< HEAD
            Spices spices = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Spices WHERE ImageName = @ImageName";
=======
            Spices dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Spices WHERE ImageName = @ImageName";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
<<<<<<< HEAD
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
=======
                    dryFruits = new Spices
                    {
                        ImageName = reader["ImageName"].ToString(),
                        ImagePath = reader["ImagePath"]?.ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToInt32(reader["Price"])
                    };
                    if (reader["ImageFile"] != DBNull.Value)
                    {
                        byte[] imageData = (byte[])reader["ImageFile"];
                        string base64String = Convert.ToBase64String(imageData);
                        dryFruits.ImagePath = "data:image/png;base64," + base64String;
                    }
                }
                connection.Close();
            }
            return View(dryFruits);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
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

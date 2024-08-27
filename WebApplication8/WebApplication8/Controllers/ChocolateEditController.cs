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
<<<<<<< HEAD
            var chocolatesList = new List<Chocolate>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate";
=======
            var dryFruitsList = new List<Chocolate>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Chocolate";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
<<<<<<< HEAD
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
=======
                    var dryFruits = new Chocolate
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

        // GET: ChocolateEdit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChocolateEdit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
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
=======
        public ActionResult Create(Chocolate dryFruits)
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
                    string query = "INSERT INTO Chocolate (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", chocolate.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", chocolate.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", chocolate.Description);
                    command.Parameters.AddWithValue("@Price", chocolate.Price);
                    command.Parameters.AddWithValue("@ShortStory", chocolate.ShortStory ?? (object)DBNull.Value);
=======
                    string query = "INSERT INTO Chocolate (ImageFile, ImageName, ImagePath, Description, Price) VALUES (@ImageFile, @ImageName, @ImagePath, @Description, @Price)";
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
            return View(chocolate);
=======
            return View(dryFruits);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
        }

        // GET: ChocolateEdit/Edit
        public ActionResult Edit(string imageName)
        {
<<<<<<< HEAD
            Chocolate chocolate = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate WHERE ImageName = @ImageName";
=======
            Chocolate dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Chocolate WHERE ImageName = @ImageName";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
<<<<<<< HEAD
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
=======
                    dryFruits = new Chocolate
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

        // POST: ChocolateEdit/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
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
=======
        public ActionResult Edit(Chocolate dryFruits)
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
                    string query = "UPDATE Chocolate SET ImagePath = @ImagePath, Description = @Description, Price = @Price, ShortStory = @ShortStory WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", chocolate.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", chocolate.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", chocolate.Description);
                    command.Parameters.AddWithValue("@Price", chocolate.Price);
                    command.Parameters.AddWithValue("@ShortStory", chocolate.ShortStory ?? (object)DBNull.Value);
=======
                    string query = "UPDATE Chocolate SET ImageFile = @ImageFile, ImagePath = @ImagePath, Description = @Description, Price = @Price WHERE ImageName = @ImageName";
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
            return View(chocolate);
=======
            return View(dryFruits);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
        }

        // GET: ChocolateEdit/Delete
        public ActionResult Delete(string imageName)
        {
<<<<<<< HEAD
            Chocolate chocolate = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate WHERE ImageName = @ImageName";
=======
            Chocolate dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Chocolate WHERE ImageName = @ImageName";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
<<<<<<< HEAD
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
=======
                    dryFruits = new Chocolate
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

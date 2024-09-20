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
=======
<<<<<<< HEAD
=======
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
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
            var chocolatesList = new List<Chocolate>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate";
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
            var dryFruitsList = new List<Chocolate>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Chocolate";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
=======
<<<<<<< HEAD
=======
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
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
                    string query = "INSERT INTO Chocolate (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", chocolate.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", chocolate.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", chocolate.Description);
                    command.Parameters.AddWithValue("@Price", chocolate.Price);
                    command.Parameters.AddWithValue("@ShortStory", chocolate.ShortStory ?? (object)DBNull.Value);
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
                    string query = "INSERT INTO Chocolate (ImageFile, ImageName, ImagePath, Description, Price) VALUES (@ImageFile, @ImageName, @ImagePath, @Description, @Price)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageFile", imageData ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
<<<<<<< HEAD
            return View(chocolate);
=======
<<<<<<< HEAD
            return View(chocolate);
=======
<<<<<<< HEAD
            return View(chocolate);
=======
<<<<<<< HEAD
            return View(chocolate);
=======
<<<<<<< HEAD
            return View(chocolate);
=======
<<<<<<< HEAD
            return View(chocolate);
=======
            return View(dryFruits);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
        }

        // GET: ChocolateEdit/Edit
        public ActionResult Edit(string imageName)
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
            Chocolate chocolate = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate WHERE ImageName = @ImageName";
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
            Chocolate dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Chocolate WHERE ImageName = @ImageName";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
        }

        // POST: ChocolateEdit/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
                    string query = "UPDATE Chocolate SET ImagePath = @ImagePath, Description = @Description, Price = @Price, ShortStory = @ShortStory WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ImageName", chocolate.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", chocolate.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", chocolate.Description);
                    command.Parameters.AddWithValue("@Price", chocolate.Price);
                    command.Parameters.AddWithValue("@ShortStory", chocolate.ShortStory ?? (object)DBNull.Value);
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
                    string query = "UPDATE Chocolate SET ImageFile = @ImageFile, ImagePath = @ImagePath, Description = @Description, Price = @Price WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageFile", imageData ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
<<<<<<< HEAD
            return View(chocolate);
=======
<<<<<<< HEAD
            return View(chocolate);
=======
<<<<<<< HEAD
            return View(chocolate);
=======
<<<<<<< HEAD
            return View(chocolate);
=======
<<<<<<< HEAD
            return View(chocolate);
=======
<<<<<<< HEAD
            return View(chocolate);
=======
            return View(dryFruits);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
        }

        // GET: ChocolateEdit/Delete
        public ActionResult Delete(string imageName)
        {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
            Chocolate chocolate = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM Chocolate WHERE ImageName = @ImageName";
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
            Chocolate dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Chocolate WHERE ImageName = @ImageName";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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

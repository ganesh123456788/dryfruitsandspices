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
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits";
=======
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits";
=======
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits";
=======
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits";
=======
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM DryFruits";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var dryFruits = new DryFruits
                    {
                        ImageName = reader["ImageName"].ToString(),
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
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty,
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0,
                        ShortStory = reader["ShortStory"] != DBNull.Value ? reader["ShortStory"].ToString() : string.Empty
                    };
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
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
                string imagePath = null;
                if (dryFruits.ImageFile != null)
                {
                    string fileName = Path.GetFileName(dryFruits.ImageFile.FileName);
                    imagePath = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    dryFruits.ImageFile.SaveAs(imagePath);
                    dryFruits.ImagePath = "/Content/" + fileName;
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
<<<<<<< HEAD
                    string query = "INSERT INTO DryFruits (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                    SqlCommand command = new SqlCommand(query, connection);

=======
<<<<<<< HEAD
                    string query = "INSERT INTO DryFruits (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                    SqlCommand command = new SqlCommand(query, connection);

=======
<<<<<<< HEAD
                    string query = "INSERT INTO DryFruits (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                    SqlCommand command = new SqlCommand(query, connection);

=======
<<<<<<< HEAD
                    string query = "INSERT INTO DryFruits (ImageName, ImagePath, Description, Price, ShortStory) VALUES (@ImageName, @ImagePath, @Description, @Price, @ShortStory)";
                    SqlCommand command = new SqlCommand(query, connection);

=======
                    string query = "INSERT INTO DryFruits (ImageFile, ImageName, ImagePath, Description, Price) VALUES (@ImageFile, @ImageName, @ImagePath, @Description, @Price)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageFile", imageData ?? (object)DBNull.Value);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);
<<<<<<< HEAD
                    command.Parameters.AddWithValue("@ShortStory", dryFruits.ShortStory ?? (object)DBNull.Value);
=======
<<<<<<< HEAD
                    command.Parameters.AddWithValue("@ShortStory", dryFruits.ShortStory ?? (object)DBNull.Value);
=======
<<<<<<< HEAD
                    command.Parameters.AddWithValue("@ShortStory", dryFruits.ShortStory ?? (object)DBNull.Value);
=======
<<<<<<< HEAD
                    command.Parameters.AddWithValue("@ShortStory", dryFruits.ShortStory ?? (object)DBNull.Value);
=======
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b

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
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits WHERE ImageName = @ImageName";
=======
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits WHERE ImageName = @ImageName";
=======
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits WHERE ImageName = @ImageName";
=======
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits WHERE ImageName = @ImageName";
=======
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM DryFruits WHERE ImageName = @ImageName";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dryFruits = new DryFruits
                    {
                        ImageName = reader["ImageName"].ToString(),
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
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty,
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0,
                        ShortStory = reader["ShortStory"] != DBNull.Value ? reader["ShortStory"].ToString() : string.Empty
                    };
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
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
                string imagePath = dryFruits.ImagePath;
                if (dryFruits.ImageFile != null)
                {
                    string fileName = Path.GetFileName(dryFruits.ImageFile.FileName);
                    imagePath = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    dryFruits.ImageFile.SaveAs(imagePath);
                    dryFruits.ImagePath = "/Content/" + fileName;
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
<<<<<<< HEAD
                    string query = "UPDATE DryFruits SET ImagePath = @ImagePath, Description = @Description, Price = @Price, ShortStory = @ShortStory WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);

=======
<<<<<<< HEAD
                    string query = "UPDATE DryFruits SET ImagePath = @ImagePath, Description = @Description, Price = @Price, ShortStory = @ShortStory WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);

=======
<<<<<<< HEAD
                    string query = "UPDATE DryFruits SET ImagePath = @ImagePath, Description = @Description, Price = @Price, ShortStory = @ShortStory WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);

=======
<<<<<<< HEAD
                    string query = "UPDATE DryFruits SET ImagePath = @ImagePath, Description = @Description, Price = @Price, ShortStory = @ShortStory WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);

=======
                    string query = "UPDATE DryFruits SET ImageFile = @ImageFile, ImagePath = @ImagePath, Description = @Description, Price = @Price WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageFile", imageData ?? (object)DBNull.Value);
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);
<<<<<<< HEAD
                    command.Parameters.AddWithValue("@ShortStory", dryFruits.ShortStory ?? (object)DBNull.Value);
=======
<<<<<<< HEAD
                    command.Parameters.AddWithValue("@ShortStory", dryFruits.ShortStory ?? (object)DBNull.Value);
=======
<<<<<<< HEAD
                    command.Parameters.AddWithValue("@ShortStory", dryFruits.ShortStory ?? (object)DBNull.Value);
=======
<<<<<<< HEAD
                    command.Parameters.AddWithValue("@ShortStory", dryFruits.ShortStory ?? (object)DBNull.Value);
=======
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b

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
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits WHERE ImageName = @ImageName";
=======
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits WHERE ImageName = @ImageName";
=======
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits WHERE ImageName = @ImageName";
=======
<<<<<<< HEAD
                string query = "SELECT ImageName, ImagePath, Description, Price, ShortStory FROM DryFruits WHERE ImageName = @ImageName";
=======
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM DryFruits WHERE ImageName = @ImageName";
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dryFruits = new DryFruits
                    {
                        ImageName = reader["ImageName"].ToString(),
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
                        ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty,
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : string.Empty,
                        Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0,
                        ShortStory = reader["ShortStory"] != DBNull.Value ? reader["ShortStory"].ToString() : string.Empty
                    };
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
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

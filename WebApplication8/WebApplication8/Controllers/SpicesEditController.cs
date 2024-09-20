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
<<<<<<< HEAD
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
=======
<<<<<<< HEAD
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
=======
<<<<<<< HEAD
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
=======
<<<<<<< HEAD
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
=======
<<<<<<< HEAD
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
=======
        private string connectionString = ConfigurationManager.ConnectionStrings["SpicesDBConnectionString"].ConnectionString;
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849

        // GET: SpicesEdit/Index
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
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
        public ActionResult Create(Spices spices)
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
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
        }

        // GET: SpicesEdit/Edit
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
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
        }

        // POST: SpicesEdit/Edit
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
        public ActionResult Edit(Spices spices)
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
=======
<<<<<<< HEAD
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
        }

        // GET: SpicesEdit/Delete
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
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
        }

        // POST: SpicesEdit/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string imageName)
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
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
=======
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
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
>>>>>>> 269e04670fc86126a2f86c6e82c9c95d19c9c894
>>>>>>> 021045f318c5c29aec4347f6ce09adbc8b00f79b
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
    }
}

using System;
<<<<<<< HEAD
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SQLTableProject.Controllers
{
    public class TableController : Controller
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
=======
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class TableController : Controller
    {
        private readonly string _connectionString = "Data Source=DESKTOP-5M6SBGL;Initial Catalog=webapps;Integrated Security=True";
        private readonly string _imagesPath = "~/Content/";

        // GET: Table/ListTables
        public ActionResult ListTables()
        {
            var tables = new List<TableDetail>();
            string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var tableName = reader.GetString(0);
                                var imageData = GetImageDataForTable(tableName);

                                tables.Add(new TableDetail
                                {
                                    TableName = tableName,
                                    ImageData = imageData?.Item1,
                                    ImageMimeType = imageData?.Item2
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error (use a logging framework)
                ViewBag.Message = "Error retrieving tables.";
            }

            return View(tables);
        }
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda

        // GET: Table/CreateTable
        public ActionResult CreateTable()
        {
            return View();
        }

        // POST: Table/CreateTable
        [HttpPost]
<<<<<<< HEAD
        public ActionResult CreateTable(string tableName, string columns, HttpPostedFileBase imageFile)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columns))
            {
                ViewBag.Message = "Table name and columns cannot be empty.";
                return View();
            }

            // Validate table name (Only allow letters, numbers, and underscores)
            if (!Regex.IsMatch(tableName, @"^[a-zA-Z_][a-zA-Z0-9_]*$"))
            {
                ViewBag.Message = "Invalid table name. Only letters, numbers, and underscores are allowed.";
                return View();
            }

            // Validate columns
            if (!Regex.IsMatch(columns, @"^[a-zA-Z0-9_,\s\(\)]+$"))
            {
                ViewBag.Message = "Invalid column definition.";
                return View();
            }

            string imagePath = null;

            // Handle image file upload
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                try
                {
                    // Define the directory to save the images
                    string imageDir = Server.MapPath("~/Images/");
                    if (!Directory.Exists(imageDir))
                    {
                        Directory.CreateDirectory(imageDir);
                    }

                    // Save the file with a unique name to avoid collisions
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    string fullFileName = fileName + "_" + DateTime.Now.Ticks + extension;
                    imagePath = Path.Combine("/Images/", fullFileName);
                    string fullPath = Path.Combine(imageDir, fullFileName);

                    // Save the file to the server
                    imageFile.SaveAs(fullPath);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error uploading image: " + ex.Message;
                    return View();
                }
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Query to create the table
                    string query = $"CREATE TABLE {tableName} ({columns})";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    // Optionally, store image path related to this table in a separate table if needed
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        string insertImageQuery = "INSERT INTO TablePhotos (TableName, ImagePath) VALUES (@TableName, @ImagePath)";
                        using (SqlCommand cmd = new SqlCommand(insertImageQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@TableName", tableName);
                            cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

                ViewBag.Message = "Table and image created successfully!";
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "Error creating table: " + ex.Message;
            }

            return View();
=======
        public ActionResult CreateTable(TableCreateModel model)
        {
            if (ModelState.IsValid)
            {
                string query = $"CREATE TABLE [{model.TableName}] (Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(100))";

                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                    {
                        var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".gif" };
                        var fileExtension = Path.GetExtension(model.ImageFile.FileName).ToLower();
                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            ModelState.AddModelError("ImageFile", "Invalid file type.");
                            return View(model);
                        }

                        using (var binaryReader = new BinaryReader(model.ImageFile.InputStream))
                        {
                            var imageData = binaryReader.ReadBytes(model.ImageFile.ContentLength);
                            var imageMimeType = model.ImageFile.ContentType;

                            SaveImageToDatabase(model.TableName, imageData, imageMimeType);
                        }
                    }

                    return RedirectToAction("ListTables");
                }
                catch (Exception ex)
                {
                    // Log error (use a logging framework)
                    ViewBag.Message = "Error creating table.";
                }
            }

            return View(model);
        }

        // GET: Table/GetImage/5
        public ActionResult GetImage(string tableName)
        {
            var imageData = GetImageDataForTable(tableName);

            if (imageData != null)
            {
                return File(imageData.Item1, imageData.Item2);
            }
            else
            {
                return HttpNotFound();
            }
        }

        private Tuple<byte[], string> GetImageDataForTable(string tableName)
        {
            byte[] imageData = null;
            string imageMimeType = null;

            string query = "SELECT ImageData, ImageMimeType FROM TableImages WHERE TableName = @TableName";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TableName", tableName);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                imageData = (byte[])reader["ImageData"];
                                imageMimeType = reader["ImageMimeType"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error (use a logging framework)
                // Handle error
            }

            return new Tuple<byte[], string>(imageData, imageMimeType);
        }

        private void SaveImageToDatabase(string tableName, byte[] imageData, string imageMimeType)
        {
            string query = "INSERT INTO TableImages (TableName, ImageData, ImageMimeType) VALUES (@TableName, @ImageData, @ImageMimeType)";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TableName", tableName);
                        command.Parameters.AddWithValue("@ImageData", imageData);
                        command.Parameters.AddWithValue("@ImageMimeType", imageMimeType);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error (use a logging framework)
                // Handle error
            }
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
        }
    }
}

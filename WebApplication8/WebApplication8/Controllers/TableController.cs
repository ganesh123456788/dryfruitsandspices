using System;
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

        // GET: Table/CreateTable
        public ActionResult CreateTable()
        {
            return View();
        }

        // POST: Table/CreateTable
        [HttpPost]
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
        }
    }
}

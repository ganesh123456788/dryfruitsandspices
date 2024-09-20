using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
=======
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
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849

namespace WebApplication8.Controllers
{
    public class TableController : Controller
    {
<<<<<<< HEAD
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
=======
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
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849

        // GET: Table/CreateTable
        public ActionResult CreateTable()
        {
            return View();
        }

        // POST: Table/CreateTable
        [HttpPost]
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
                    string imageDir = Server.MapPath("/Content/");
=======
                    string imageDir = Server.MapPath("~/Images/");
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
                    if (!Directory.Exists(imageDir))
                    {
                        Directory.CreateDirectory(imageDir);
                    }

                    // Save the file with a unique name to avoid collisions
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    string fullFileName = fileName + "_" + DateTime.Now.Ticks + extension;
<<<<<<< HEAD
                    imagePath = Path.Combine("/Content/", fullFileName);
=======
                    imagePath = Path.Combine("/Images/", fullFileName);
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
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
<<<<<<< HEAD
                return RedirectToAction("InsertData"); // Redirect to InsertData
=======
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "Error creating table: " + ex.Message;
            }

            return View();
<<<<<<< HEAD
        }

        // GET: Table/InsertData
        public ActionResult InsertData()
        {
            ViewBag.Tables = GetTableNames(); // Populate the tables dropdown
            return View();
        }

        // POST: Table/InsertData
        [HttpPost]
        public ActionResult InsertData(string tableName, FormCollection formCollection, string imageName, HttpPostedFileBase imageFile, decimal price)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                ViewBag.Message = "Please select a table.";
                ViewBag.Tables = GetTableNames(); // Re-populate tables list on failure
                return View();
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Prepare insert statement
                    List<string> columnNames = new List<string>();
                    List<string> columnValues = new List<string>();

                    // Loop through the formCollection to get column names and values
                    foreach (var key in formCollection.Keys)
                    {
                        if (key.ToString().StartsWith("col_"))
                        {
                            string columnName = key.ToString().Substring(4); // Remove 'col_' prefix
                            string value = formCollection[key.ToString()];
                            columnNames.Add(columnName);
                            columnValues.Add("'" + value + "'"); // Wrap values in quotes for SQL statement
                        }
                    }

                    // Include image name and price
                    columnNames.Add("ImageName");
                    columnNames.Add("Price");
                    columnValues.Add($"'{imageName}'");
                    columnValues.Add(price.ToString());

                    // Build SQL query
                    string query = $"INSERT INTO {tableName} ({string.Join(", ", columnNames)}) VALUES ({string.Join(", ", columnValues)})";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                ViewBag.Message = "Data inserted successfully!";
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "Error inserting data: " + ex.Message;
            }

            ViewBag.Tables = GetTableNames(); // Re-populate tables list after insert
            return View();
        }

        // GET: Table/DisplayTables
        public ActionResult DisplayTables()
        {
            List<TableWithImage> tablesWithImages = new List<TableWithImage>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to get the tables and associated images from the TablePhotos table
                    string query = "SELECT TableName, ImagePath FROM TablePhotos";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tablesWithImages.Add(new TableWithImage
                                {
                                    TableName = reader["TableName"].ToString(),
                                    ImagePath = reader["ImagePath"].ToString()
                                });
                            }
                        }
                    }

                    con.Close();
=======
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
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
                }
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                ViewBag.Message = "Error fetching table data: " + ex.Message;
            }

            return View(tablesWithImages);
        }

        // Class to hold table data and image information
        public class TableWithImage
        {
            public string TableName { get; set; }
            public string ImagePath { get; set; }
        }

        // Utility method to get all table names from the database
        private List<string> GetTableNames()
        {
            List<string> tables = new List<string>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    DataTable schema = con.GetSchema("Tables");

                    foreach (DataRow row in schema.Rows)
                    {
                        tables.Add(row[2].ToString()); // Table name is in the 3rd column
                    }

                    con.Close();
=======
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
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
                }
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                ViewBag.Message = "Error fetching tables: " + ex.Message;
            }

            return tables;
        }

        // AJAX request to get table structure
        [HttpPost]
        public JsonResult GetTableStructure(string tableName)
        {
            List<ColumnInfo> columns = new List<ColumnInfo>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to get the column names and types
                    string query = $"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TableName", tableName);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                columns.Add(new ColumnInfo
                                {
                                    ColumnName = reader["COLUMN_NAME"].ToString(),
                                    DataType = reader["DATA_TYPE"].ToString()
                                });
                            }
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // Log exception (you can add logging here)
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true, data = columns });
        }

        public class ColumnInfo
        {
            public string ColumnName { get; set; }
            public string DataType { get; set; }
=======
                // Log error (use a logging framework)
                // Handle error
            }
>>>>>>> 08f4c18630278d7eca78f7aecd599abc28350bda
>>>>>>> b1dc5a96c2a12a21bcf2f299120505f9b24b2849
        }
    }
}

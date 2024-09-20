using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WebApplication8.Controllers
{
    public class TableController : Controller
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        // GET: Table/CreateTable
        public ActionResult CreateTable()
        {
            return View();
        }

        // POST: Table/CreateTable
        [HttpPost]
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
                    string imageDir = Server.MapPath("/Content/");
                    if (!Directory.Exists(imageDir))
                    {
                        Directory.CreateDirectory(imageDir);
                    }

                    // Save the file with a unique name to avoid collisions
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    string fullFileName = fileName + "_" + DateTime.Now.Ticks + extension;
                    imagePath = Path.Combine("/Content/", fullFileName);
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
                return RedirectToAction("InsertData"); // Redirect to InsertData
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "Error creating table: " + ex.Message;
            }

            return View();
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
                }
            }
            catch (Exception ex)
            {
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
                }
            }
            catch (Exception ex)
            {
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
        }
    }
}

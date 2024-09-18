using System;
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
        }
    }
}

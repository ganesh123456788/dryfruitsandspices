﻿using System;
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
            var dryFruitsList = new List<Spices>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Spices";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
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
        }

        // GET: SpicesEdit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpicesEdit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Spices (ImageFile, ImageName, ImagePath, Description, Price) VALUES (@ImageFile, @ImageName, @ImagePath, @Description, @Price)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageFile", imageData ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
            return View(dryFruits);
        }

        // GET: SpicesEdit/Edit
        public ActionResult Edit(string imageName)
        {
            Spices dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Spices WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
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
        }

        // POST: SpicesEdit/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Spices SET ImageFile = @ImageFile, ImagePath = @ImagePath, Description = @Description, Price = @Price WHERE ImageName = @ImageName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImageFile", imageData ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImageName", dryFruits.ImageName);
                    command.Parameters.AddWithValue("@ImagePath", dryFruits.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", dryFruits.Description);
                    command.Parameters.AddWithValue("@Price", dryFruits.Price);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
            return View(dryFruits);
        }

        // GET: SpicesEdit/Delete
        public ActionResult Delete(string imageName)
        {
            Spices dryFruits = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ImageFile, ImageName, ImagePath, Description, Price FROM Spices WHERE ImageName = @ImageName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ImageName", imageName);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
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

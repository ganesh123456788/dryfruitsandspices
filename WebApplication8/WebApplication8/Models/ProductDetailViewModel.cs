using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class ProductDetailViewModel
    {
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ProductType { get; set; } // "DryFruits", "Spices", or "Chocolates"
        public string ShortStory { get; set; } // New property
    }

}
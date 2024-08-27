using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class DryFruits
    {
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
<<<<<<< HEAD
        public string ShortStory { get; set; } 
=======
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApplication8.Models;

namespace WebApplication8.Models
{
    public class Chocolate
    {
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
<<<<<<< HEAD
        public string ShortStory { get; set; }
=======
<<<<<<< HEAD
        public string ShortStory { get; set; }
=======
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1
>>>>>>> 901688282898ff11154d4a648ba17e842570c831
        public string Description { get; set; }
        public Decimal Price { get; set; }
    }
}
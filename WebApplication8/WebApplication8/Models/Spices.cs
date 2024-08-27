<<<<<<< HEAD
﻿using System.Web;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1

namespace WebApplication8.Models
{
    public class Spices
    {
<<<<<<< HEAD
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ShortStory { get; set; }  
        public HttpPostedFileBase ImageFile { get; set; }  // For file upload
    }
}
=======
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

>>>>>>> 7e3f928faabd10c9f152b7c1de955ce83682f9a1

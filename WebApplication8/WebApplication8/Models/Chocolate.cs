﻿using System;
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
        public string ShortStory { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
    }
}
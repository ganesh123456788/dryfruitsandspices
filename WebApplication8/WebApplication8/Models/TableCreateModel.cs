using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class TableCreateModel
    {
        public string TableName { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
using System.Web;

namespace WebApplication8.Models
{
    public class Spices
    {
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ShortStory { get; set; }  
        public HttpPostedFileBase ImageFile { get; set; }  // For file upload
    }
}

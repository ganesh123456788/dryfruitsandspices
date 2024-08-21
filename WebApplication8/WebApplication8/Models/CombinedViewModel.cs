using System.Collections.Generic;
using WebApplication8.Models;

namespace WebApplication8.Models
{
    public class CombinedViewModel
    {
        public List<Spices> SpicesList { get; set; }
        public List<DryFruits> DryFruitsList { get; set; }
        public List<Chocolate> ChocolatesList { get; set; }
    }
}

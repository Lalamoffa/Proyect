using FLO_Proyect.Models;

namespace FLO_Proyect.ViewModel
{
    public class ProductVM
    {
        public Products Product { get; set; }
        public List<int> ColorIds { get; set; }
        public List<int> SizeIds { get; set; } = new List<int>();
        public List<string> Images { get; set; } = new List<string>();
    }
}

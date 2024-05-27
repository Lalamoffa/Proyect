using FLO_Proyect.Models;
using Stripe;

namespace FLO_Proyect.ViewModel
{
    public class HomeVM
    {
        public List<Products> Products { get; set; }
        public List<Slider> Sliders { get; set; }
    }
}

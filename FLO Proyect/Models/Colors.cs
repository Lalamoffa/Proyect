namespace FLO_Proyect.Models
{
    public class Colors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ColorToProduct> ColorToProducts { get; set; }

    }
}

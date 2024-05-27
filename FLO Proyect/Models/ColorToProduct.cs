using System.ComponentModel.DataAnnotations.Schema;

namespace FLO_Proyect.Models
{
    public class ColorToProduct
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        [ForeignKey("ColorId")]
        public Colors Colors { get; set; }
    }
}

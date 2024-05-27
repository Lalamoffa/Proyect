using System.ComponentModel.DataAnnotations.Schema;

namespace FLO_Proyect.Models
{
    public class SizeToProduct
    {
        public int Id { get; set; }
        public int SizeId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        [ForeignKey("SizeId")]
        public Size Size { get; set; }




    }
}

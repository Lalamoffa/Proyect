using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLO_Proyect.Models
{
    public class Orders
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

    }
}

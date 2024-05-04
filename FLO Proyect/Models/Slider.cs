using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLO_Proyect.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public bool Ischeck { get; set; } = false;
        [NotMapped]
        public IFormFile ImgFile { get; set; }
       
    }
}

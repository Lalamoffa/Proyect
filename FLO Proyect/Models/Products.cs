using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLO_Proyect.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? OldPrice { get; set; } = 0;
        public string? ImgUrlBase { get; set; }
        public bool Ischeck { get; set; } = true;
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public int CategoryId { get; set; }
        [ValidateNever]
        public List<ColorToProduct> ColorToProducts { get; set; }
        [ValidateNever]
        public List<SizeToProduct> SizeToProducts { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public Orders Orders { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }
        [ValidateNever]
        public List<Images> Images { get; set; }
        [NotMapped]
        [Required]
        public IFormFile ImgUrlBaseFile { get; set; }
        [NotMapped]
        [ValidateNever]
        public List<IFormFile> ImagesFile { get; set; }
        [NotMapped]
        [ValidateNever]
        public List<int> ColorsId { get; set; }

    }
}

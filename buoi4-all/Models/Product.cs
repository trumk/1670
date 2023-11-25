using System.ComponentModel.DataAnnotations.Schema;

namespace buoi4_all.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; }
        public string? Picture { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}

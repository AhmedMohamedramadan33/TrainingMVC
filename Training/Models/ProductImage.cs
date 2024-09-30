 using System.ComponentModel.DataAnnotations.Schema;

namespace Training.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        [ForeignKey("product")]
        public int ProductId { get; set; }
        public Product? product { get; set; }
    }
}

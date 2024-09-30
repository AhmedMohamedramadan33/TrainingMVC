using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Training.Helpers;

namespace Training.Models
{
    public class Product:LocalizableEntity
    {
        public int Id { get; set; }

        public string NameEn { get; set; }

        public string NameAr { get; set; }

        public decimal Price { get; set; }    
        [ForeignKey(nameof(Category))]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public ICollection<ProductImage> Images { get; set; }=new HashSet<ProductImage>();
        public Category? Category { get; set; }
    }
}
 
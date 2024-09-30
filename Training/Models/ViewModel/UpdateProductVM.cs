using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Training.Models.ViewModel
{
    public class UpdateProductVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "NameEnglishIsRequired")]
        [DisplayName("Name Engilsh")]
        public string NameEn { get; set; }
        [Remote("IsproductNameArExist", "Product", HttpMethod = "Post", ErrorMessage = "ThisProductAlreadyExist")]
        [Required(ErrorMessage = "NameArabicIsRequired")]
        [DisplayName("Name Arabic")]

        public string NameAr { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        [DisplayName("Image")]
        public List<IFormFile>? formFiles { get; set; }
        [DisplayName("Category")]
        public List<String>? CurrentPaths { get; set; }
        public int CategoryId { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Training.Models.ViewModel
{
    public class AddProductVM
    {
        
        [Remote("IsproductNameEnExist", "Product", HttpMethod = "Post", ErrorMessage = "ThisProductAlreadyExist")]
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
        public int CategoryId { get; set; }

    }
}

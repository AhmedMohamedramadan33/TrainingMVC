using Training.Helpers;

namespace Training.Models
{
    public class Category:LocalizableEntity
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public ICollection<Product> Products { get; set; }= new HashSet<Product>();
    }
}

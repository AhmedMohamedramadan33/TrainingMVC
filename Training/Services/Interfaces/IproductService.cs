using Training.Models;
using Training.Models.data;
using Training.Models.ViewModel;

namespace Training.Services.Interfaces
{
    public interface IproductService
    {
        public  Task<String> Add(Product product,List<IFormFile> formFiles);
        public  Task<string> Delete(Product product);
        public  Task<Product?> GetById(int id);
        public  Task<List<Product>> GettAll();
        public  Task<String> Update(Product product, List<IFormFile> formFile);
        public  Task<bool> IsProductNameEnExist(string productname);
        public Task<bool> IsProductNameArExist(string productname);
        public Task<Product?> GetByIdWithInclude(int id);
    }
}

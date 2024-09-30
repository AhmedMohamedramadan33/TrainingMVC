using Training.Models;

namespace Training.Services.Interfaces
{
    public interface IcategoryService
    {
        public Task<List<Category>> GetAll();
    }
}

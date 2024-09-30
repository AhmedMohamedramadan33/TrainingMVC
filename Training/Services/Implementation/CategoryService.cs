using Microsoft.EntityFrameworkCore;
using Training.Models;
using Training.Models.data;
using Training.Services.Interfaces;

namespace Training.Services.Implementation
{
    public class CategoryService : IcategoryService
    {
        private readonly IfileService _IfileService;
        private readonly AppDBContext _AppDBContext;
        public CategoryService(IfileService ifileService, AppDBContext AppDBContext)
        {

            _IfileService = ifileService;
            _AppDBContext = AppDBContext;
        }
        public async Task<List<Category>> GetAll()
        {
            return await _AppDBContext.categories.ToListAsync();
                
        }
    }
}

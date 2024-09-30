using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Training.Migrations;
using Training.Models;
using Training.Models.data;
using Training.Services.Interfaces;

namespace Training.Services.Implementation
{
    public class ProductService : IproductService
    {
        private readonly IfileService _IfileService;
        private readonly AppDBContext _AppDBContext;


        public ProductService(IfileService ifileService, AppDBContext AppDBContext)
        {
            _IfileService = ifileService;
            _AppDBContext = AppDBContext;
        }
        public async Task<String> Add(Product product, List<IFormFile> formFile)
        {
            var PathList = new List<string>();
            var TransactionDb = await _AppDBContext.Database.BeginTransactionAsync();
            try
            {
                await _AppDBContext.Products.AddAsync(product);
                await _AppDBContext.SaveChangesAsync();
                var res = await AddProductImage(formFile, product.Id);
                if (res.Item1 == null && res.Item2 != "Success") { return "failed"; }
                PathList = res.Item1;
                await TransactionDb.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await TransactionDb.RollbackAsync();
                foreach (var i in PathList)
                {
                    _IfileService.DeletePhysicalPath(i);
                }
                return "Erorr";
            }
        }

        public async Task<string> Delete(Product product)
        {
            try
            {
                //var path = "";
                if (product != null)
                {
                    //path = product.PathFile;
                    _AppDBContext.Products.Remove(product);
                    //_IfileService.DeletePhysicalPath(path);
                    await _AppDBContext.SaveChangesAsync();
                    return "success";
                }
                return "Error";
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        public async Task<Product?> GetById(int id)
        {
            return await _AppDBContext.Products.FindAsync(id);
        }
        public async Task<Product?> GetByIdWithInclude(int id)
        {
            return await _AppDBContext.Products.Include(z => z.Images).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Product>> GettAll()
        {
            return await _AppDBContext.Products.ToListAsync();
        }



        public async Task<bool> IsProductNameArExist(string NameAr)
        {
            return await _AppDBContext.Products.AnyAsync(x => x.NameAr == NameAr);
        }

        public async Task<bool> IsProductNameEnExist(string NameEn)
        {
            return await _AppDBContext.Products.AnyAsync(x => x.NameEn == NameEn);
        }

        public async Task<String> Update(Product product, List<IFormFile> formFile)
        {
            var PathList = new List<string>();
            var TransactionDb = await _AppDBContext.Database.BeginTransactionAsync();
            try

            {
                var model = await GetById(product.Id);
                if (model == null) { return "Failed"; }

                _AppDBContext.Products.Update(product);
                await _AppDBContext.SaveChangesAsync();
                if (formFile != null && formFile.Count() > 0)
                {
                    var ProductImage = await _AppDBContext.ProductImages.Where(x => x.Id == product.Id).ToListAsync();
                    if (ProductImage.Count > 0)
                    {
                        var pathes = ProductImage.Select(x => x.Path).ToList();
                        _AppDBContext.ProductImages.RemoveRange(ProductImage);
                        await _AppDBContext.SaveChangesAsync();
                        //delete physical
                        foreach (var i in pathes)
                        {
                            _IfileService.DeletePhysicalPath(i);
                        }
                    }
                    var res = await AddProductImage(formFile, product.Id);
                    if (res.Item1 == null&&res.Item2!="Success") { return "failed"; }
                    PathList = res.Item1;

                }
                await TransactionDb.CommitAsync();
                return "Success";
            }

            catch (Exception)
            {
                await TransactionDb.RollbackAsync();
                foreach (var i in PathList)
                {
                    _IfileService.DeletePhysicalPath(i);
                }
                return "Erorr";
            }
        }



        private async Task<(List<string>,string)> AddProductImage(List<IFormFile> formFile, int ProductId)
        {
            var PathList = new List<string>();

            if (formFile != null && formFile.Count() > 0)
            {
                foreach (var file in formFile)
                {
                    string path = await _IfileService.UploadFile(file, "/images/");
                    if (!path.StartsWith("/images/"))
                    {
                        return (null,path);
                    }
                    PathList.Add(path);
                }
                var productimage = new List<ProductImage>();
                foreach (var i in PathList)
                {
                    var productImg = new ProductImage()
                    {
                        ProductId = ProductId,
                        Path = i,

                    };
                    productimage.Add(productImg);

                }
                _AppDBContext.ProductImages.AddRange(productimage);
                await _AppDBContext.SaveChangesAsync();
            }
            return (PathList,"Success");
        }
    }


}
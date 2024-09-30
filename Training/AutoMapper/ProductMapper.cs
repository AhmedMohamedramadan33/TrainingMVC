using AutoMapper;
using Training.Models;
using Training.Models.ViewModel;

namespace Training.AutoMapper
{
    public class ProductMapper:Profile
    {
        public ProductMapper() {
            CreateMap<AddProductVM, Product>().ReverseMap();
            CreateMap<Product, GetProducts>().ForMember(des => des.Name, src => src.MapFrom(t => t.Localize(t.NameAr, t.NameEn))).ReverseMap();
            CreateMap<Product, UpdateProductVM>().ForMember(des=>des.CurrentPaths,src=>src.MapFrom(x=>x.Images.Select(z=>z.Path).ToList()));
        }
    }
}

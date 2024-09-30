using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Training.Models;
using Training.Models.ViewModel;
using Training.Services.Interfaces;

namespace Training.Controllers
{
    public class ProductController : Controller
    {
        private readonly IproductService _iproductservice;
        private readonly IfileService _ifileService;
        private readonly IcategoryService _icategoryService;
        private readonly IMapper _autoMapper;
        public ProductController(IproductService iproductservice, IfileService ifileService, IcategoryService icategoryService, IMapper autoMapper)
        {
            _iproductservice = iproductservice;
            _ifileService = ifileService;
            _icategoryService= icategoryService;
            _autoMapper = autoMapper;
        }
        public async Task<IActionResult> Index()
        {
            var All =await _iproductservice.GettAll();
            var Products=_autoMapper.Map<List<GetProducts>>(All);
            return View(Products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = new SelectList(await _icategoryService.GetAll(), "Id", "NameEn");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddProductVM addProductVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = _autoMapper.Map<Product>(addProductVM);
                  
            var res=       await _iproductservice.Add(product,addProductVM.formFiles);
                    if(res== "Success")
                    {
                        TempData["Success"] = "Success";
                        return RedirectToAction(nameof(Index));
                    }
                    TempData["Failed"] = "Failed";
                    ViewData["Categories"] = new SelectList(await _icategoryService.GetAll(), "Id", "Name");
                    return View(product);
                }
                TempData["Failed"] = "Failed";
                ViewData["Categories"] = new SelectList(await _icategoryService.GetAll(), "Id", "Name");
                return View(addProductVM);
            }
            catch (Exception )
            {
              TempData["Failed"] = "Failed";                
                ViewData["Categories"] = new SelectList(await _icategoryService.GetAll(), "Id", "Name");
                return View(addProductVM);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model =await _iproductservice.GetByIdWithInclude(id);
            if (model != null)
            {
                ViewData["Categories"] = new SelectList(await _icategoryService.GetAll(), "Id", "NameEn");
                var product = _autoMapper.Map<UpdateProductVM>(model);
                return View(product);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateProductVM product)
        {
            try
            {
                if (id != product.Id)
                {
                    return NotFound();
                }
                var model=await _iproductservice.GetById(id);
                if (model == null) { return NotFound(); }
               var newmodel=_autoMapper.Map<Product>(product);
                            
          var res=     await _iproductservice.Update(newmodel,product.formFiles);
                if(res== "Success")
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Categories"] = new SelectList(await _icategoryService.GetAll(), "Id", "NameEn");
                return View(product);
            }
            catch (Exception)
            {
                ViewData["Categories"] = new SelectList(await _icategoryService.GetAll(), "Id", "NameEn");
                return View(product);
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            var product =await _iproductservice.GetById(id);
            if (product != null)
            {
                return View(product);
            }
            return NotFound();
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult>ConfirmDelete(int id)
        {
            var model =await _iproductservice.GetById(id);
            if (model != null)
            {
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task <IActionResult> Delete(int id)
         {
            try
            {
              var  product=await _iproductservice.GetById(id);
                if (product==null) return NotFound();
                //if (model != null)
                //{
                //    model.PathFile = product.PathFile;
                //    model.Price = product.Price;
                //    model.Name = product.Name;
                //}
                var res = await  _iproductservice.Delete(product);
                if(res== "success")
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            catch (Exception)
            {
                return View();
            }
        }
        public async Task<IActionResult> IsproductNameArExist(string NameAr)
        {
            var res =await _iproductservice.IsProductNameArExist(NameAr);
            if (res)
            {
                return Json(false);
            }
            return Json(true);

        }
        public async Task<IActionResult> IsproductNameEnExist(string NameEn)
        {
            var res = await _iproductservice.IsProductNameEnExist(NameEn);
            if (res)
            {
                return Json(false);
            }
            return Json(true);

        }
       
    }
}

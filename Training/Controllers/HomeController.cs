using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.Models.data;

namespace Training.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _appDBContext;
        private string Name = "ahmed mohamed";
        public HomeController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task<IActionResult> Index()
        {
            var categoris=await _appDBContext.categories.Include(x=>x.Products.Take(5)).ToListAsync();
            return View(categoris);
        }
        public IActionResult ShowData()
        {
            var Data = Request.Cookies["Nameck"];
            ViewBag.Data= Data;
            return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}

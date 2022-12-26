using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ComesticShop.Pages.Salers.ProductPage
{
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IndexModel(IAccountRepository accountRepository, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task OnGet(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Categories = await _categoryRepository.GetCategoriesVer2();
            Product = await _productRepository.GetProductByCategoryId(id);
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("CusID");
            return RedirectToPage("/Authenticate/Login");
        }
    }
}

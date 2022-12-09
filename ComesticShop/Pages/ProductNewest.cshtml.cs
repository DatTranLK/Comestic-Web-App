using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages
{
    public class ProductNewestModel : PageModel
    {
        private readonly ILogger<ProductNewestModel> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public string Msg { get; set; }
        public Account Account { get; set; }

        public string Email { get; set; }
        public string Role { get; set; }

        public IEnumerable<Product> ProductsNewest { get; set; }
        public IEnumerable<Category> Category1 { get; set; }
        public IEnumerable<Category> Category2 { get; set; }

        public ProductNewestModel(ILogger<ProductNewestModel> logger, IAccountRepository accountRepository, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task OnGet()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Category1 = await _categoryRepository.GetCategoriesByType(1);
            Category2 = await _categoryRepository.GetCategoriesByType(2);
            ProductsNewest = await _productRepository.GetListProductNewest();
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("cartCus");
            HttpContext.Session.Remove("CusID");
            return RedirectToPage("/Index");
        }
    }
}

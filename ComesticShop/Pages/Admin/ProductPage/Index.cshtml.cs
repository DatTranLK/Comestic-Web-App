using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.ProductPage
{
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;
        private readonly ITypeRepository _typeRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }
        public IndexModel(IAccountRepository accountRepository, IProductRepository productRepository, ITypeRepository typeRepository)
        {
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _typeRepository = typeRepository;
        }
        public async Task OnGet()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Products = await _productRepository.GetProducts();
            Type = await _typeRepository.GetListTypes();
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("CusID");
            HttpContext.Session.Clear();
            return RedirectToPage("/Authenticate/Login");
        }
    }
}

using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComesticShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;

        public string Msg { get; set; }
        public Account Account { get; set; }

        public string Email { get; set; }
        public string Role { get; set; }

        public IEnumerable<Product> ProductsAmountSold { get; set; }
        public IEnumerable<Product> ProductsNewest { get; set; }
        public IEnumerable<Product> ProductsRecommend { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IAccountRepository accountRepository, IProductRepository productRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _productRepository = productRepository;
        }

        public async Task OnGetAsync()
        {
            
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            ProductsAmountSold = await _productRepository.GetProductAmountSold();
            ProductsNewest = await _productRepository.GetProductNewest();
            ProductsRecommend = await _productRepository.GetProductRecomend();
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("cartCus");
            HttpContext.Session.Remove("CusID");
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}

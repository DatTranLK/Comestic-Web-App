using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace ComesticShop.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Category> Category1 { get; set; }
        public IEnumerable<Category> Category2 { get; set; }
        public IEnumerable<Product> RelatedProduct { get; set; }
        public ProductDetailModel(IAccountRepository accountRepository, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category1 = await _categoryRepository.GetCategoriesByType(1);
            Category2 = await _categoryRepository.GetCategoriesByType(2);
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            if (id == null)
            {
                return NotFound();
            }

            Product = await _productRepository.GetProductById(id);
            var x = await _productRepository.GetProductByCategoryId((int)Product.CategoryId);
            RelatedProduct = x.Take(3);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

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
    public class SearchResultPageModel : PageModel
    {
        private readonly ILogger<SearchResultPageModel> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public string Msg { get; set; }
        public Account Account { get; set; }

        public string Email { get; set; }
        public string Role { get; set; }
        public string? searchString { get; set; }

        public IEnumerable<Category> Category1 { get; set; }
        public IEnumerable<Category> Category2 { get; set; }

/*        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }*/
        public IEnumerable<Product> Product { get; set; }

        public SearchResultPageModel(ILogger<SearchResultPageModel> logger, IAccountRepository accountRepository, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task OnGetAsync(string searchStringre)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Category1 = await _categoryRepository.GetCategoriesByType(1);
            Category2 = await _categoryRepository.GetCategoriesByType(2);
            searchString = searchStringre;
            if (!string.IsNullOrEmpty(searchStringre))
            {
                Product = await _productRepository.Search(searchStringre);
                if (Product == null)
                {
                    Msg = "No product has found";
                }
            }
        }
    }
}

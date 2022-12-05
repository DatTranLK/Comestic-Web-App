using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.BrandPage
{
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ITypeRepository _typeRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public IEnumerable<Brand> Brand { get; set; }
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }
        public IndexModel(IAccountRepository accountRepository, IBrandRepository brandRepository, ITypeRepository typeRepository)
        {
            _accountRepository = accountRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
        }
        public async Task OnGet()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Brand = await _brandRepository.GetBrands();
            Type = await _typeRepository.GetListTypes();
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

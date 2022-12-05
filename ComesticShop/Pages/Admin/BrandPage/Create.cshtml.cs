using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.BrandPage
{
    public class CreateModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ITypeRepository _typeRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        [BindProperty]
        public Brand Brand { get; set; }
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }
        public CreateModel(IAccountRepository accountRepository, IBrandRepository brandRepository, ITypeRepository typeRepository)
        {
            _accountRepository = accountRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
        }
        public async Task<IActionResult> OnGet()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Brand.IsActive = true;
            await _brandRepository.AddNewBrand(Brand);

            return RedirectToPage("./Index");
        }
    }
}

using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.BrandPage
{
    public class EditModel : PageModel
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
        public EditModel(IAccountRepository accountRepository, IBrandRepository brandRepository, ITypeRepository typeRepository)
        {
            _accountRepository = accountRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            if (id == null)
            {
                return NotFound();
            }

            Brand = await _brandRepository.GetBrandById(id);

            if (Brand == null)
            {
                return NotFound();
            }
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
            await _brandRepository.UpdateBrand(Brand);

            return RedirectToPage("./Index");
        }
    }
}

using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.BrandPage
{
    public class DetailsModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ITypeRepository _typeRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public Brand Brand { get; set; }
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }
        public DetailsModel(IAccountRepository accountRepository, IBrandRepository brandRepository, ITypeRepository typeRepository)
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
    }
}

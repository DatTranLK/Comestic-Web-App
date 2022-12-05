using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.TypePage
{
    public class EditModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITypeRepository _typeRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        [BindProperty]
        public BusinessObject.Models.Type Type1 { get; set; }
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }

        public EditModel(IAccountRepository accountRepository, ITypeRepository typeRepository)
        {
            _accountRepository = accountRepository;
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

            Type1 = await _typeRepository.GetTypeById(id);

            if (Type == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Type1.IsActive = true;
            await _typeRepository.UpdateType(Type1);
            return RedirectToPage("./Index");
        }
    }
}

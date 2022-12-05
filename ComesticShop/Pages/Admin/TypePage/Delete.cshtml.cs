using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.TypePage
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public BusinessObject.Models.Type Type1 { get; set; }
        private readonly IAccountRepository _accountRepository;
        private readonly ITypeRepository _typeRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }

        public DeleteModel(IAccountRepository accountRepository, ITypeRepository typeRepository)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Type1 = await _typeRepository.GetTypeById(id);
            if (Type1 == null)
            {
                return NotFound();
            }

            await _typeRepository.DeleteType(id);

            return RedirectToPage("./Index");
        }
    }
}

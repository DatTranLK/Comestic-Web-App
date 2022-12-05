using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.AccountPage
{
    public class EditModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly ComesticDBContext _context;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        [BindProperty]
        public Account Accounts { get; set; }
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }
        public EditModel(IAccountRepository accountRepository, ITypeRepository typeRepository, ComesticDBContext context)
        {
            _accountRepository = accountRepository;
            _typeRepository = typeRepository;
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            ViewData["RoleId"] = new SelectList(_context.Roles.Where(x => x.Name != "Admin"), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            Accounts = await _accountRepository.GetAccountById(id);

            if (Accounts == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            ViewData["RoleId"] = new SelectList(_context.Roles.Where(x => x.Name != "Admin"), "Id", "Name");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var checkExist = await _accountRepository.GetAccountById(Accounts.Id);
            if (checkExist == null)
            {
                return NotFound();
            }
            await _accountRepository.UpdateAccount(Accounts);

            return RedirectToPage("./Index");
        }
    }
}

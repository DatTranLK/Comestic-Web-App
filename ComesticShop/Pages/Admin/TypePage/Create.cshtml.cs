using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.TypePage
{
    public class CreateModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITypeRepository _typeRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }
        public CreateModel(IAccountRepository accountRepository, ITypeRepository typeRepository)
        {
            _accountRepository = accountRepository;
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

        [BindProperty]
        public BusinessObject.Models.Type Type1 { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
            if (Type1.Name.StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "The Name of the Type has whitespace in the head!!! Please try again";
                return Page();
            }
            Regex r = new Regex(@"^[a-zA-Z0-9\s]+$");
            if (!r.IsMatch(Type1.Name))
            {
                ViewData["ErrorMessage"] = "The Name of the Type has special characters!!! Please try again";
                return Page();
            }
            Type1.IsActive = true;
            await _typeRepository.AddNewType(Type1);

            return RedirectToPage("./Index");
        }
    }
}

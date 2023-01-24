using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.AccountPage
{
    public class CreateModel : PageModel
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
        public CreateModel(IAccountRepository accountRepository, ITypeRepository typeRepository, ComesticDBContext context)
        {
            _accountRepository = accountRepository;
            _typeRepository = typeRepository;
            _context = context;
        }
        public async Task<IActionResult> OnGet()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            ViewData["RoleId"] = new SelectList(_context.Roles.Where(x => x.Name != "Admin"), "Id", "Name");
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
            if (Accounts.Password.Length < 6 || Accounts.Password.Length > 20)
            {
                ViewData["ErrorMessage"] = "Password must have between 6 to 20 Characters!!! Please try again";
                return Page();
            }
            Regex r = new Regex(@"^[a-zA-Z0-9\s]+$");
            if (Accounts.Name.StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "Name has whitespace in the head!!! Please try again";
                return Page();
            }
            if (!r.IsMatch(Accounts.Name))
            {
                ViewData["ErrorMessage"] = "Name has special characters!!! Please try again";
                return Page();
            }
            if (Accounts.Name.Length > 20)
            {
                ViewData["ErrorMessage"] = "Name Has More Than 50 Characters!!! Please try again";
                return Page();
            }

            Regex phoneRegex = new Regex(@"^[0-9]{10}$");
            if (!phoneRegex.IsMatch(Accounts.Phone))
            {
                ViewData["ErrorMessage"] = "The Phone Number is wrong format or the length of the phone is greater than 10 !!! Please try again";
                return Page();
            }
            if (Accounts.Phone.StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "Phone has whitespace in the head!!! Please try again";
                return Page();
            }
            if (Accounts.DateOfBirth > DateTime.UtcNow)
            {
                ViewData["ErrorMessage"] = "The Date of Birth is greater than now !!! Please try again";
                return Page();
            }
            Accounts.IsActive = true;
            await _accountRepository.AddNewAccount(Accounts);

            return RedirectToPage("./Index");
        }
    }
}

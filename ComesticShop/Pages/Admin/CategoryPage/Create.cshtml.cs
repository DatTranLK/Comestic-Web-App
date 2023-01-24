using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.CategoryPage
{
    public class CreateModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly ComesticDBContext _context;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }
        public CreateModel(IAccountRepository accountRepository, ICategoryRepository categoryRepository, ITypeRepository typeRepository, ComesticDBContext context)
        {
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _typeRepository = typeRepository;
            _context = context;
        }
        public async Task<IActionResult> OnGet()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Regex r = new Regex(@"^[a-zA-Z0-9\s]+$");
            if (!r.IsMatch(Category.Name))
            {
                ViewData["ErrorMessage"] = "The Name of the Category has special characters!!! Please try again";
                return Page();
            }
            if (Category.Name.StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "The Name of the Category has whitespace in the head!!! Please try again";
                return Page();
            }
            Category.IsActive = true;
            await _categoryRepository.AddNewCate(Category);

            return RedirectToPage("./Index");
        }
    }
}

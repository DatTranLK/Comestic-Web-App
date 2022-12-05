using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.CategoryPage
{
    public class EditModel : PageModel
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
        [BindProperty]
        public Category Category { get; set; }
        public EditModel(IAccountRepository accountRepository, ICategoryRepository categoryRepository, ITypeRepository typeRepository, ComesticDBContext context)
        {
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _typeRepository = typeRepository;
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            Category = await _categoryRepository.GetCategoryById(id);

            if (Category == null)
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
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Category.IsActive = true;
            await _categoryRepository.UpdateCate(Category);
            

            return RedirectToPage("./Index");
        }
    }
}

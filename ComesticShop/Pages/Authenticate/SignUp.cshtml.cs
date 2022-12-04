using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Authenticate
{
    public class SignUpModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        [BindProperty]
        [Required]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        public string Password { get; set; }
        [BindProperty]
        [Required]
        public string Name { get; set; }
        public string Message { get; set; }
        public SignUpModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var checkExist = await _accountRepository.GetAccountByEmail(Email);
            if (checkExist != null)
            {
                Message = "The email has existed, Please enter new email";
                return Page();
            }

            Account currentAccount = await _accountRepository.Register(Name, Email, Password);

            if (currentAccount != null)
            {
                HttpContext.Session.SetString("Email", currentAccount.Email);
                HttpContext.Session.SetString("Role", currentAccount.RoleId.ToString());
                return RedirectToPage("/Index");
            }
            return Page();

        }
    }
}

using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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
            Regex r = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
            if (Name.StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "Name have whitespace!!! Please try again";
                return Page();
            }
            if (!r.IsMatch(Name))
            {
                ViewData["ErrorMessage"] = "Name have special characters!!! Please try again";
                return Page();
            }
            if (Name.Length > 20)
            {
                ViewData["ErrorMessage"] = "Name Has More Than 50 Characters!!! Please try again";
                return Page();
            }
            if (Email.Length > 50)
            {
                ViewData["ErrorMessage"] = "Email Has More Than 50 Characters!!! Please try again";
                return Page();
            }
            if (Password.Length > 20 || Password.Length < 6)
            {
                ViewData["ErrorMessage"] = "Password must have between 6 to 20 Characters!!! Please try again";
                return Page();
            }
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

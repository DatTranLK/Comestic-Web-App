using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Authenticate
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        [BindProperty]
        [Required]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
        public LoginModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

        }
        public void OnGet()
        {
            HttpContext.Session.Clear();
        }
        public async Task<IActionResult> OnPost()
        {
            var account = await _accountRepository.CheckLogin(Email, Password);
            /*if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ViewData["ErrorMessage"] = "Email Or Password Have WhiteSpace!!!";
                return Page();
            }*/
            if (Email.Length > 50)
            {
                ViewData["ErrorMessage"] = "Email Has More Than 50 Characters!!!";
                return Page();
            }
            if (Password.Length > 20 || Password.Length < 6)
            {
                ViewData["ErrorMessage"] = "Password must have between 6 to 20 Characters!!!";
                return Page();
            }

            if (account != null)
            {
                if (account.RoleId == 1)
                {
                    HttpContext.Session.SetString("Email", account.Email);
                    HttpContext.Session.SetString("Role", account.RoleId.ToString());
                    HttpContext.Session.SetString("CusID", account.Id.ToString());
                    return RedirectToPage("/Admin/ProductPage/Index");
                }
                if (account.RoleId == 2)
                {
                    HttpContext.Session.SetString("Email", account.Email);
                    HttpContext.Session.SetString("Role", account.RoleId.ToString());
                    HttpContext.Session.SetString("CusID", account.Id.ToString());
                    return RedirectToPage("/Salers/ProductPage/Index", new { id = 1 });
                }
                if (account.RoleId == 3)
                {
                    HttpContext.Session.SetString("Email", account.Email);
                    HttpContext.Session.SetString("Role", account.RoleId.ToString());
                    HttpContext.Session.SetString("CusID", account.Id.ToString());
                    return RedirectToPage("/Index");
                }

            }
            else
            {
                ViewData["ErrorMessage"] = "Login Unsuccessfully!!!";
                return Page();
            }

            return Page();
        }
    }
}

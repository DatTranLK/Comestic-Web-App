using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Salers.ProductPage
{
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public IndexModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task OnGet()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            /*Book = _bookRepository.GetBooks();
            Store = _storeRepository.GetStoresNoDes();
            if (Store == null)
            {
                Msg = "There is no store in here";
            }
            if (Book == null)
            {
                Msg = "There is no book in here";
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                List<Book> bookSearchList = _bookRepository.SearchBook(SearchString.Trim()).ToList();
                Book = bookSearchList;
            }*/
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("CusID");
            return RedirectToPage("/Authenticate/Login");
        }
    }
}

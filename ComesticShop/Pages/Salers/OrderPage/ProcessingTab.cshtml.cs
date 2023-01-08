using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Salers.OrderPage
{
    public class ProcessingTabModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ITypeRepository _typeRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public IEnumerable<Order> Order { get; set; }
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public ProcessingTabModel(ICategoryRepository categoryRepository, IAccountRepository accountRepository, IOrderRepository orderRepository, ITypeRepository typeRepository)
        {
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _orderRepository = orderRepository;
            _typeRepository = typeRepository;
        }
        public async Task OnGetAsync()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Categories = await _categoryRepository.GetCategoriesVer2();
            Type = await _typeRepository.GetListTypes();
            Order = await _orderRepository.GetOrdersProcessingIn7Days();
            
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

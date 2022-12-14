using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Salers.OrderPage
{
    public class OrderDetailPageModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ITypeRepository _typeRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public OrderDetailPageModel(ICategoryRepository categoryRepository, IAccountRepository accountRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, ITypeRepository typeRepository)
        {
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _orderDetailRepository = orderDetailRepository;
            _typeRepository = typeRepository;
        }
        public async Task OnGetAsync(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Categories = await _categoryRepository.GetCategoriesVer2();
            Type = await _typeRepository.GetListTypes();
            OrderDetail = await _orderDetailRepository.GetOrderDetailById(id);
        }
    }
}

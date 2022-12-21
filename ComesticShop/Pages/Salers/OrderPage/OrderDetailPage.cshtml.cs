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
        private readonly IAccountRepository _accountRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
        public OrderDetailPageModel(IAccountRepository accountRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _accountRepository = accountRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task OnGetAsync(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            OrderDetail = await _orderDetailRepository.GetOrderDetailById(id);
        }
    }
}

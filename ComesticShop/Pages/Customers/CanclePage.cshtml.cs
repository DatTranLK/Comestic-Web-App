using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Customers
{
    public class CanclePageModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICategoryRepository _categoryRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        [BindProperty]
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
        public IEnumerable<Category> Category1 { get; set; }
        public IEnumerable<Category> Category2 { get; set; }
        public CanclePageModel(IAccountRepository accountRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, ICategoryRepository categoryRepository)
        {
            _accountRepository = accountRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            OrderDetail = await _orderDetailRepository.GetOrderDetailById(id);
            Category1 = await _categoryRepository.GetCategoriesByType(1);
            Category2 = await _categoryRepository.GetCategoriesByType(2);
            if (OrderDetail == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            OrderDetail = await _orderDetailRepository.GetOrderDetailById(id);
            Category1 = await _categoryRepository.GetCategoriesByType(1);
            Category2 = await _categoryRepository.GetCategoriesByType(2);
            if (id == null)
            {
                return NotFound();
            }
            var checkOrderExist = await _orderRepository.GetOrderById(id);
            
            if (checkOrderExist != null && checkOrderExist.OrderStatus != "Processing" && checkOrderExist.OrderStatus != "Cancle")
            {
                ViewData["ErrorMessage"] = "Đơn hàng không thể hủy khi đã được Accept";
                return Page();
            }
            if (checkOrderExist.OrderStatus != "Processing")
            {
                ViewData["ErrorMessage"] = "Đơn hàng đã hủy";
                return Page();
            }
            await _orderRepository.ChangeStatusToCancle(id);

            return RedirectToPage("./OrderHistory");
        }
    }
}

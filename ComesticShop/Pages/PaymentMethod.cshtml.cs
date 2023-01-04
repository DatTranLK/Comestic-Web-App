using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages
{
    public class PaymentMethodModel : PageModel
    {
        private readonly ILogger<PaymentMethodModel> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IVnPayService _vnPayService;
        private readonly IOrderRepository _orderRepository;
        private readonly ComesticDBContext _context;

        public string Msg { get; set; }
        public Account Account { get; set; }

        public string Email { get; set; }
        public string Role { get; set; }

        public Order OrderNek { get; set; }
        public Order OrderNek2 { get; set; }
        public IEnumerable<Category> Category1 { get; set; }
        public IEnumerable<Category> Category2 { get; set; }

        public PaymentMethodModel(ILogger<PaymentMethodModel> logger, IAccountRepository accountRepository, IProductRepository productRepository, ICategoryRepository categoryRepository, IVnPayService vnPayService, IOrderRepository orderRepository, ComesticDBContext context)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _vnPayService = vnPayService;
            _orderRepository = orderRepository;
            _context = context;
        }

        public async Task OnGetAsync(int orderId)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Category1 = await _categoryRepository.GetCategoriesByType(1);
            Category2 = await _categoryRepository.GetCategoriesByType(2);
            OrderNek = await _orderRepository.GetOrderById(orderId);
        }
        public async Task<IActionResult> OnGetPaymentvnpay(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Category1 = await _categoryRepository.GetCategoriesByType(1);
            Category2 = await _categoryRepository.GetCategoriesByType(2);
            OrderNek2 = await _orderRepository.GetOrderById(id);
            OrderNek2.PaymentMethod = "Thanh toán online";
            await _context.SaveChangesAsync();
            /*var url = _vnPayService.CreatePaymentUrl(OrderNek2, HttpContext);

            return Redirect(url);*/
            return RedirectToPage("/Customers/OrderDetailPage", new { id = OrderNek2.Id });
        }
        public async Task<IActionResult> OnGetPaymentcod(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Category1 = await _categoryRepository.GetCategoriesByType(1);
            Category2 = await _categoryRepository.GetCategoriesByType(2);
            OrderNek2 = await _orderRepository.GetOrderById(id);
            OrderNek2.PaymentMethod = "Thanh toán khi nhận hàng";
            await _context.SaveChangesAsync();
            return RedirectToPage("/Customers/OrderDetailPage", new { id = OrderNek2.Id});
        }
    }
}

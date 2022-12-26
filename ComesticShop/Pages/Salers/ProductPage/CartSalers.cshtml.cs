using BusinessObject.Models;
using ComesticShop.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Salers.ProductPage
{
    public class CartSalersModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICategoryRepository _categoryRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public List<Item> cartSaler { get; set; }
        public Product Product { get; set; }
        public Account Account { get; set; }
        [BindProperty]
        public decimal? Total { get; set; } = 0;
        public string ShippingAddress { get; set; }

        public CartSalersModel(IAccountRepository accountRepository, IProductRepository productRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, ICategoryRepository categoryRepository)
        {
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task OnGet(int? id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            cartSaler = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cartSaler");
            if (id == null)
            {
                cartSaler = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cartSaler");

            }
            else
            {
                var product = await _productRepository.GetProductById((int)id);
                if (cartSaler == null)
                {
                    cartSaler = new List<Item>();
                    cartSaler.Add(new Item
                    {
                        Product = product,
                        Quantity = 1
                    });
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cartSaler", cartSaler);
                }
                else
                {
                    int index = Exists(cartSaler, (int)id);
                    if (index == -1)
                    {
                        cartSaler.Add(new Item
                        {
                            Product = product,
                            Quantity = 1
                        });
                    }
                    else
                    {
                        cartSaler[index].Quantity++;
                    }

                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cartSaler", cartSaler);
                }
            }


        }

        private int Exists(List<Item> cart, int id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public async Task OnGetDelete(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            cartSaler = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cartSaler");
            int index = Exists(cartSaler, id);
            cartSaler.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartSaler", cartSaler);
        }

        public async Task OnGetIncrease(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            cartSaler = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cartSaler");
            int index = Exists(cartSaler, id);
            cartSaler[index].Quantity++;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartSaler", cartSaler);
        }

        public async Task OnGetDecrease(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            cartSaler = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cartSaler");
            int index = Exists(cartSaler, id);
            cartSaler[index].Quantity--;
            if (cartSaler[index].Quantity <= 0)
            {
                OnGetDelete(id);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartSaler", cartSaler);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            var totalString = Request.Form["SubTotal"];
            decimal total;
            decimal? result = null;
            if (decimal.TryParse((string)totalString, out total))
                result = total;
            var ShippingAddressString = Request.Form["SubShippingAddr"];

            cartSaler = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cartSaler");
            var staff = await _accountRepository.GetAccountByEmail(Email);
            var order = new Order();
            order.CreateDate = DateTime.UtcNow.AddHours(7);
            order.CustomerId = null;
            order.StaffId = staff.Id;
            order.ShippingAddress = ShippingAddressString;
            order.TotalPrice = total;
            order.OrderStatus = "Done";
            int orderId = await _orderRepository.CreateNewOrder(order);
            for (int i = 0; i < cartSaler.Count; i++)
            {
                var checkOutOfStock = await _productRepository.GetProductById(cartSaler[i].Product.Id);
                if (checkOutOfStock.Amount <= 0 || checkOutOfStock.Amount - cartSaler[i].Quantity < 0)
                {
                    ViewData["ErrorMessage"] = "Số lượng hàng không đủ để đáp ứng";
                    return Page();
                }
                await _orderDetailRepository.AddNewOrderDetail(cartSaler[i].Quantity, orderId, cartSaler[i].Product.Id);
            }
            cartSaler.Clear();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartSaler", cartSaler);
            return RedirectToPage("./Index");

        }
    }
}

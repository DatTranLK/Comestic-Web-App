using BusinessObject.Models;
using ComesticShop.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages
{
    public class CartModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public List<Item> cartCus { get; set; }
        public Product Product { get; set; }
        public Account Account { get; set; }
        [BindProperty]
        public decimal? Total { get; set; } = 0;
        [BindProperty]
        public decimal? ShippingFee { get; set; } = 30000;
        public string ShippingAddress { get; set; }

        public CartModel(IAccountRepository accountRepository, IProductRepository productRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task OnGet(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            cartCus = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cartCus");

            var product = await _productRepository.GetProductById(id);
            if (cartCus == null)
            {
                cartCus = new List<Item>();
                cartCus.Add(new Item
                {
                    Product = product,
                    Quantity = 1
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartCus", cartCus);

            }
            else
            {
                int index = Exists(cartCus, id);
                if (index == -1)
                {
                    cartCus.Add(new Item
                    {
                        Product = product,
                        Quantity = 1
                    });
                }
                else
                {
                    cartCus[index].Quantity++;
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartCus", cartCus);
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
            cartCus = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cartCus");
            int index = Exists(cartCus, id);
            cartCus.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartCus", cartCus);
        }

        public async Task OnGetIncrease(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            cartCus = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cartCus");
            int index = Exists(cartCus, id);
            cartCus[index].Quantity++;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartCus", cartCus);
        }

        public async Task OnGetDecrease(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            cartCus = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cartCus");
            int index = Exists(cartCus, id);
            cartCus[index].Quantity--;
            if (cartCus[index].Quantity <= 0)
            {
                OnGetDelete(id);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartCus", cartCus);
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

            cartCus = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cartCus");
            var customer = await _accountRepository.GetAccountByEmail(Email);
            var order = new Order();
            order.CreateDate = DateTime.Now;
            order.StaffId = null;
            order.CustomerId = customer.Id;
            order.ShippingAddress = ShippingAddressString;
            order.TotalPrice = total;
            order.OrderStatus = "Processing";
            int orderId = await _orderRepository.CreateNewOrder(order);
            for (int i = 0; i < cartCus.Count; i++)
            {
                var checkOutOfStock = await _productRepository.GetProductById(cartCus[i].Product.Id);
                if (checkOutOfStock.Amount <= 0 || cartCus[i].Quantity - checkOutOfStock.Amount < 0)
                {
                    ViewData["ErrorMessage"] = "Số lượng hàng không đủ để đáp ứng";
                    return Page();
                }
                await _orderDetailRepository.AddNewOrderDetail(cartCus[i].Quantity, orderId, cartCus[i].Product.Id);
            }
            cartCus.Clear();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartCus", cartCus);
            return RedirectToPage("./Index");

        }
    }
}

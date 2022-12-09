﻿using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Salers.OrderPage
{
    public class DoneModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        [BindProperty]
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
        public DoneModel(IAccountRepository accountRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _accountRepository = accountRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
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
            if (id == null)
            {
                return NotFound();
            }
            var checkOrderExist = await _orderRepository.GetOrderById(id);
            if (checkOrderExist != null && checkOrderExist.OrderStatus == "Cancle" || checkOrderExist != null && checkOrderExist.OrderStatus == "Processing")
            {
                ViewData["ErrorMessage"] = "Đơn hàng không thể hoàn thành khi đơn hàng đã hủy";
                return Page();
            }
            await _orderRepository.ChangeStatusToDone(id);

            return RedirectToPage("./Index");
        }
    }
}
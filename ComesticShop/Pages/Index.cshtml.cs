﻿using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComesticShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAccountRepository _accountRepository;

        public string Msg { get; set; }
        public Account Account { get; set; }

        public string Email { get; set; }
        public string Role { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }

        public async Task OnGet()
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            /*Store = _storeRepository.GetStoresNoDes();
            Books = _bookRepository.GetBooksInStore();
            if (Store == null)
            {
                Msg = "There is no store in here";
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                List<Book> bookSearchList = _bookRepository.SearchBook(SearchString.Trim()).ToList();
                Books = bookSearchList;
            }*/


        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("cartCus");
            HttpContext.Session.Remove("CusID");
            return RedirectToPage("/Index");
        }
    }
}
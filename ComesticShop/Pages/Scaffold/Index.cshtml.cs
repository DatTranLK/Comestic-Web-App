using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace ComesticShop.Pages.Scaffold
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Models.ComesticDBContext _context;

        public IndexModel(BusinessObject.Models.ComesticDBContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; }

        public async Task OnGetAsync()
        {
            Account = await _context.Accounts
                .Include(a => a.Role).ToListAsync();
        }
    }
}

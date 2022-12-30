using BusinessObject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.AddProductWithType
{
    public class CreateModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IProductRepository _productRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly ComesticDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public string Email { get; set; }
        public string Role { get; set; }
        public Account Account { get; set; }
        public string Msg { get; set; }
        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public IFormFile Img1 { get; set; }
        [BindProperty]
        public IFormFile? Img2 { get; set; } = null;
        [BindProperty]
        public IFormFile? Img3 { get; set; } = null;
        [BindProperty]
        public IFormFile? Img4 { get; set; } = null;
        [BindProperty]
        public IFormFile? Img5 { get; set; } = null;
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }
        public CreateModel(IAccountRepository accountRepository, IProductRepository productRepository, ITypeRepository typeRepository, ComesticDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _typeRepository = typeRepository;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.TypeId == id), "Id", "Name");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.TypeId == id), "Id", "Name");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Img1.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    Img1.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    Product.Image1 = fileBytes;
                }
            }
            if (Img2 == null)
            {
                Product.Image2 = null;
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    Img2.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    Product.Image2 = fileBytes;
                }
            }

            if (Img3 == null)
            {
                Product.Image3 = null;

            }
            else 
            {
                using (var ms = new MemoryStream())
                {
                    Img3.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    Product.Image3 = fileBytes;
                }
            }

            if (Img4 == null)
            {
                Product.Image4 = null;
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    Img4.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    Product.Image4 = fileBytes;
                }
            }

            if (Img5 == null)
            {
                Product.Image5 = null;
            }
            else 
            {
                using (var ms = new MemoryStream())
                {
                    Img5.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    Product.Image5 = fileBytes;
                }
            }
            /*if (Img5.Length > 0 && Img5 != null)
            {

            }*/
            Product.IsActive = true;
            await _productRepository.AddNewProduct(Product);

            return RedirectToPage("/Admin/ProductPage/Index");
        }

        
    }
}

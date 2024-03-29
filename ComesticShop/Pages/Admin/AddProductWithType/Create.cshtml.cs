 using BusinessObject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            var checkExistWithCode = await _context.Products.FirstOrDefaultAsync(x => x.Code == Product.Code);
            if (checkExistWithCode != null)
            {
                ViewData["ErrorMessage"] = "The Code has exist!!! Please enter new code";
                return Page();
            }

            if (Product.Code.StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "Code has whitespace in the head!!! Please try again";
                return Page();
            }
            Regex r = new Regex(@"^[a-zA-Z0-9\s]+$");
            if (!r.IsMatch(Product.Code))
            {
                ViewData["ErrorMessage"] = "Code has special characters!!! Please try again";
                return Page();
            }

            if (Product.Name.StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "Name has whitespace in the head!!! Please try again";
                return Page();
            }
            
            if (!r.IsMatch(Product.Name))
            {
                ViewData["ErrorMessage"] = "Name has special characters!!! Please try again";
                return Page();
            }

            if (Product.Amount.ToString().StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "Amount has whitespace in the head!!! Please try again";
                return Page();
            }
            Regex rAmount = new Regex(@"^[0-9]*$");
            if (!rAmount.IsMatch(Product.Amount.ToString()))
            {
                ViewData["ErrorMessage"] = "Amount must be a number!!! Please try again";
                return Page();
            }
            if (Product.Amount <= 0)
            {
                ViewData["ErrorMessage"] = "Amount must greater than 0!!! Please try again";
                return Page();
            }


            if (Product.Price < 0)
            {
                ViewData["ErrorMessage"] = "The Price must equal or greater than 0!!! Please try again";
                return Page();
            }

            if (Product.Summary.StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "Summary has whitespace in the head!!! Please try again";
                return Page();
            }
           
            if (!r.IsMatch(Product.Summary))
            {
                ViewData["ErrorMessage"] = "Summary has special characters!!! Please try again";
                return Page();
            }

            if (Product.Description.StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "Description has whitespace in the head!!! Please try again";
                return Page();
            }

            if (!r.IsMatch(Product.Description))
            {
                ViewData["ErrorMessage"] = "Description has special characters!!! Please try again";
                return Page();
            }

            if (Product.Element.StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "Element has whitespace in the head!!! Please try again";
                return Page();
            }

            if (!r.IsMatch(Product.Element))
            {
                ViewData["ErrorMessage"] = "Element has special characters!!! Please try again";
                return Page();
            }

            if (Product.UserManual.StartsWith(" "))
            {
                ViewData["ErrorMessage"] = "UserManual has whitespace in the head!!! Please try again";
                return Page();
            }

            if (!r.IsMatch(Product.UserManual))
            {
                ViewData["ErrorMessage"] = "UserManual has special characters!!! Please try again";
                return Page();
            }
            string imgText1 = Path.GetExtension(Img1.FileName);
            if (imgText1 == ".jpg" || imgText1 == ".png" || imgText1 == ".jpeg")
            {
                var imgSave1 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", Img1.FileName);
                var stream = new FileStream(imgSave1, FileMode.Create);
                await Img1.CopyToAsync(stream);
                stream.Close();

                Product.ImgName1 = Img1.FileName;
                Product.ImgPath1 = imgSave1.Substring(imgSave1.IndexOf("w"));
            }
            if (Img2 == null)
            {
                Product.ImgName2 = null;
                Product.ImgPath2 = null;
            }
            else 
            {
                string imgText2 = Path.GetExtension(Img2.FileName);
                if (imgText2 == ".jpg" || imgText2 == ".png" || imgText2 == ".jpeg")
                {
                    var imgSave2 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", Img2.FileName);
                    var stream = new FileStream(imgSave2, FileMode.Create);
                    await Img2.CopyToAsync(stream);
                    stream.Close();

                    Product.ImgName2 = Img2.FileName;
                    Product.ImgPath2 = imgSave2.Substring(imgSave2.IndexOf("w"));
                }
            }

            if (Img3 == null)
            {
                Product.ImgName3 = null;
                Product.ImgPath3 = null;
            }
            else
            {
                string imgText3 = Path.GetExtension(Img3.FileName);
                if (imgText3 == ".jpg" || imgText3 == ".png" || imgText3 == ".jpeg")
                {
                    var imgSave3 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", Img3.FileName);
                    var stream = new FileStream(imgSave3, FileMode.Create);
                    await Img3.CopyToAsync(stream);
                    stream.Close();

                    Product.ImgName3 = Img3.FileName;
                    Product.ImgPath3 = imgSave3.Substring(imgSave3.IndexOf("w"));
                }
            }

            if (Img4 == null)
            {
                Product.ImgName4 = null;
                Product.ImgPath4 = null;
            }
            else
            {
                string imgText4 = Path.GetExtension(Img4.FileName);
                if (imgText4 == ".jpg" || imgText4 == ".png" || imgText4 == ".jpeg")
                {
                    var imgSave4 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", Img4.FileName);
                    var stream = new FileStream(imgSave4, FileMode.Create);
                    await Img4.CopyToAsync(stream);
                    stream.Close();

                    Product.ImgName4 = Img4.FileName;
                    Product.ImgPath4 = imgSave4.Substring(imgSave4.IndexOf("w"));
                }
            }

            if (Img5 == null)
            {
                Product.ImgName5 = null;
                Product.ImgPath5 = null;
            }
            else
            {
                string imgText5 = Path.GetExtension(Img5.FileName);
                if (imgText5 == ".jpg" || imgText5 == ".png" || imgText5 == ".jpeg")
                {
                    var imgSave5 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", Img5.FileName);
                    var stream = new FileStream(imgSave5, FileMode.Create);
                    await Img5.CopyToAsync(stream);
                    stream.Close();

                    Product.ImgName5 = Img5.FileName;
                    Product.ImgPath5 = imgSave5.Substring(imgSave5.IndexOf("w"));
                }
            }
            Product.IsActive = true;
            await _productRepository.AddNewProduct(Product);

            return RedirectToPage("/Admin/ProductPage/Index");
        }

        
    }
}

using BusinessObject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComesticShop.Pages.Admin.ProductPage
{
    public class EditModel : PageModel
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
        public Product ProductEdit { get; set; }
        public Product Product2 { get; set; }
        /*[BindProperty]
        public IFormFile Img1 { get; set; }
        [BindProperty]
        public IFormFile? Img2 { get; set; } = null;
        [BindProperty]
        public IFormFile? Img3 { get; set; } = null;
        [BindProperty]
        public IFormFile? Img4 { get; set; } = null;
        [BindProperty]
        public IFormFile? Img5 { get; set; } = null;*/
        public IEnumerable<BusinessObject.Models.Type> Type { get; set; }
        public EditModel(IAccountRepository accountRepository, IProductRepository productRepository, ITypeRepository typeRepository, ComesticDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _typeRepository = typeRepository;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            if (id == null)
            {
                return NotFound();
            }

            ProductEdit = await _productRepository.GetProductByproId(id);

            if (ProductEdit == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.TypeId == ProductEdit.Category.TypeId), "Id", "Name");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id, IFormFile Img1, IFormFile Img2, IFormFile Img3, IFormFile Img4, IFormFile Img5)
        {
            Email = HttpContext.Session.GetString("Email");
            Role = HttpContext.Session.GetString("Role");
            Account = await _accountRepository.GetAccountByEmail(Email);
            Type = await _typeRepository.GetListTypes();
            Product2 = await _productRepository.GetProductByproId(id);
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.TypeId == Product2.Category.TypeId), "Id", "Name");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Attach(ProductEdit).State = EntityState.Modified;
            try
            {
                var productNeedToDelete = await _context.Products.FindAsync(id);
                _context.Products.Remove(productNeedToDelete);
                //Image1
                if (Img1 == null)
                {
                    ProductEdit.ImgName1 = Product2.ImgName1;
                    ProductEdit.ImgPath1 = Product2.ImgPath1;
                }
                else 
                {
                    var fname = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", productNeedToDelete.ImgName1);
                    FileInfo fi = new FileInfo(fname);
                    if (fi.Exists)
                    {
                        System.IO.File.Delete(fname);
                        fi.Delete();
                    }
                    string imgText1 = Path.GetExtension(Img1.FileName);
                    if (imgText1 == ".jpg" || imgText1 == ".png" || imgText1 == ".jpeg")
                    {
                        var imgSave1 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", Img1.FileName);
                        var stream = new FileStream(imgSave1, FileMode.Create);
                        await Img1.CopyToAsync(stream);
                        stream.Close();

                        ProductEdit.ImgName1 = Img1.FileName;
                        ProductEdit.ImgPath1 = imgSave1.Substring(imgSave1.IndexOf("w"));

                    }
                }
                
                

                //Image 2
                if (Img2 == null)
                {
                    ProductEdit.ImgName2 = Product2.ImgName2;
                    ProductEdit.ImgPath2 = Product2.ImgPath2;
                }
                else 
                {
                    var fname2 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", productNeedToDelete.ImgName2);
                    FileInfo fi2 = new FileInfo(fname2);
                    if (fi2.Exists)
                    {
                        System.IO.File.Delete(fname2);
                        fi2.Delete();
                    }
                    string imgText2 = Path.GetExtension(Img2.FileName);
                    if (imgText2 == ".jpg" || imgText2 == ".png" || imgText2 == ".jpeg")
                    {
                        var imgSave2 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", Img2.FileName);
                        var stream = new FileStream(imgSave2, FileMode.Create);
                        await Img2.CopyToAsync(stream);
                        stream.Close();

                        ProductEdit.ImgName2 = Img2.FileName;
                        ProductEdit.ImgPath2 = imgSave2.Substring(imgSave2.IndexOf("w"));

                    }
                }

                //Image 3
                if (Img3 == null)
                {
                    ProductEdit.ImgName3 = Product2.ImgName3;
                    ProductEdit.ImgPath3 = Product2.ImgPath3;
                }
                else
                {
                    var fname3 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", productNeedToDelete.ImgName3);
                    FileInfo fi3 = new FileInfo(fname3);
                    if (fi3.Exists)
                    {
                        System.IO.File.Delete(fname3);
                        fi3.Delete();
                    }
                    string imgText3 = Path.GetExtension(Img3.FileName);
                    if (imgText3 == ".jpg" || imgText3 == ".png" || imgText3 == ".jpeg")
                    {
                        var imgSave3 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", Img3.FileName);
                        var stream = new FileStream(imgSave3, FileMode.Create);
                        await Img3.CopyToAsync(stream);
                        stream.Close();

                        ProductEdit.ImgName3 = Img3.FileName;
                        ProductEdit.ImgPath3 = imgSave3.Substring(imgSave3.IndexOf("w"));

                    }
                }

                //Image4
                if (Img4 == null)
                {
                    ProductEdit.ImgName4 = Product2.ImgName4;
                    ProductEdit.ImgPath4 = Product2.ImgPath4;
                }
                else
                {
                    var fname4 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", productNeedToDelete.ImgName4);
                    FileInfo fi4 = new FileInfo(fname4);
                    if (fi4.Exists)
                    {
                        System.IO.File.Delete(fname4);
                        fi4.Delete();
                    }
                    string imgText4 = Path.GetExtension(Img4.FileName);
                    if (imgText4 == ".jpg" || imgText4 == ".png" || imgText4 == ".jpeg")
                    {
                        var imgSave4 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", Img4.FileName);
                        var stream = new FileStream(imgSave4, FileMode.Create);
                        await Img4.CopyToAsync(stream);
                        stream.Close();

                        ProductEdit.ImgName4 = Img4.FileName;
                        ProductEdit.ImgPath4 = imgSave4.Substring(imgSave4.IndexOf("w"));

                    }
                }

                //Image5
                if (Img5 == null)
                {
                    ProductEdit.ImgName5 = Product2.ImgName5;
                    ProductEdit.ImgPath5 = Product2.ImgPath5;
                }
                else
                {
                    var fname5 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", productNeedToDelete.ImgName5);
                    FileInfo fi5 = new FileInfo(fname5);
                    if (fi5.Exists)
                    {
                        System.IO.File.Delete(fname5);
                        fi5.Delete();
                    }
                    string imgText5 = Path.GetExtension(Img5.FileName);
                    if (imgText5 == ".jpg" || imgText5 == ".png" || imgText5 == ".jpeg")
                    {
                        var imgSave5 = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesPro", Img5.FileName);
                        var stream = new FileStream(imgSave5, FileMode.Create);
                        await Img5.CopyToAsync(stream);
                        stream.Close();

                        ProductEdit.ImgName5 = Img5.FileName;
                        ProductEdit.ImgPath5 = imgSave5.Substring(imgSave5.IndexOf("w"));

                    }
                }
                ProductEdit.Id = id;
                ProductEdit.IsActive = true;
                /*await _productRepository.AddNewProduct(ProductEdit);*/
                await _productRepository.UpdatePro(ProductEdit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(ProductEdit.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return RedirectToPage("/Admin/ProductPage/Index");
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

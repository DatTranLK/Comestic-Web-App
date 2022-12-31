using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class ProductDAO
    {
        private static ProductDAO instance;
        private static readonly object instanceLock = new object();
        ComesticDBContext _dbContext = new ComesticDBContext();
        public ProductDAO()
        {

        }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<IEnumerable<Product>> Search(string searchString)
        {
            try
            {
                var lst = _dbContext.Products.Where(x => x.Name.Contains(searchString.Trim())).ToList();
                if (lst.Count() > 0)
                {
                    foreach (var item in lst)
                    {
                        if (item.Image1 != null)
                        {
                            item.Image1 = GetImages(Convert.ToBase64String(item.Image1));
                        }
                    }
                    return lst;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Product>> GetProductAmountSold()
        {
            try
            {
                var lst = await _dbContext.Products
                    .Include(x => x.Category)
                    .Where(x => x.IsActive == true && x.Amount > 0)
                    .OrderByDescending(x => x.AmountSold).ToListAsync();
                if (lst != null)
                {
                    
                    foreach (var item in lst)
                    {
                        if (item.Image1 != null)
                        {
                            item.Image1 = GetImages(Convert.ToBase64String(item.Image1));
                        }
                    }
                    return lst.Take(3);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public byte[] GetImages(string sBase64String) 
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(sBase64String))
            {
                bytes = Convert.FromBase64String(sBase64String);
            }
            return bytes;
        }
        public async Task<IEnumerable<Product>> GetListProductAmountSold()
        {
            try
            {
                var lst = await _dbContext.Products
                    .Where(x => x.IsActive == true && x.Amount > 0)
                    .OrderByDescending(x => x.AmountSold).ToListAsync();
                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                        if (item.Image1 != null)
                        {
                            item.Image1 = GetImages(Convert.ToBase64String(item.Image1));
                        }
                    }
                    return lst;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Product>> GetListProductForSaler()
        {
            try
            {
                var lst = await _dbContext.Products
                    .Include(x => x.Brand)
                    .Include(x => x.Category)
                    .Where(x => x.IsActive == true && x.Amount > 0)
                    .OrderByDescending(x => x.Id).ToListAsync();
                if (lst != null)
                {
                    return lst;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Product>> GetProductNewest()
        {
            try
            {
                var lst = await _dbContext.Products
                    .Where(x => x.IsActive == true && x.Amount > 0)
                    .OrderByDescending(x => x.DateCreated).ToListAsync();
                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                        if (item.Image1 != null)
                        {
                            item.Image1 = GetImages(Convert.ToBase64String(item.Image1));
                        }
                    }
                    return lst.Take(3);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetListProductNewest()
        {
            try
            {
                var lst = await _dbContext.Products
                    .Where(x => x.IsActive == true && x.Amount > 0)
                    .OrderByDescending(x => x.DateCreated).ToListAsync();
                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                        if (item.Image1 != null)
                        {
                            item.Image1 = GetImages(Convert.ToBase64String(item.Image1));
                        }
                    }
                    return lst;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Product>> GetProductRecomend()
        {
            try
            {
                var lst = await _dbContext.Products
                    .Where(x => x.IsActive == true && x.Amount > 0).ToListAsync();
                /*Random rand = new Random();
                int toSkip = rand.Next(6,lst);*/
                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                        if (item.Image1 != null)
                        {
                            item.Image1 = GetImages(Convert.ToBase64String(item.Image1));
                        }
                    }
                    return lst.OrderBy(x => Guid.NewGuid()).Take(6);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Product>> GetListProductRecomend()
        {
            try
            {
                var lst = await _dbContext.Products
                    .Where(x => x.IsActive == true && x.Amount > 0).ToListAsync();
                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                        if (item.Image1 != null)
                        {
                            item.Image1 = GetImages(Convert.ToBase64String(item.Image1));
                        }
                    }
                    return lst.OrderBy(x => Guid.NewGuid());
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                var lst = await _dbContext.Products
                    .Include(x => x.Category)
                    .Include(x => x.Brand)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                if (lst != null)
                {
                    return lst;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var pro = await _dbContext.Products
                    .Include(x => x.Category)
                    .Include(x => x.Brand)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (pro != null)
                {
                    return pro;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Product>> GetProductByCategoryId(int cateId)
        {
            try
            {
                var pro = await _dbContext.Products
                    .Include(x => x.Category)
                    .Include(x => x.Brand)
                    .Where(x => x.CategoryId == cateId && x.IsActive == true && x.Amount > 0).ToListAsync();
                if(pro != null)
                {
                    foreach (var item in pro)
                    {
                        if (item.Image1 != null)
                        {
                            item.Image1 = GetImages(Convert.ToBase64String(item.Image1));
                        }
                    }
                    return pro;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task AddNewProduct(Product product)
        {
            try
            {
                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Product> GetProductByproId(int id)
        {
            try
            {
                var pro = await _dbContext.Products
                    .Include(x => x.Category)
                    .Include(x => x.Brand)
                    .Include(x => x.Category.Type)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(pro != null)
                {

                    if (pro.Image1 != null)
                    {
                        pro.Image1 = GetImages(Convert.ToBase64String(pro.Image1));
                    }
                    if (pro.Image2 != null)
                    {
                        pro.Image2 = GetImages(Convert.ToBase64String(pro.Image2));
                    }
                    if (pro.Image3 != null)
                    {
                        pro.Image3 = GetImages(Convert.ToBase64String(pro.Image3));
                    }
                    if (pro.Image4 != null)
                    {
                        pro.Image4 = GetImages(Convert.ToBase64String(pro.Image4));
                    }
                    if (pro.Image5 != null)
                    {
                        pro.Image5 = GetImages(Convert.ToBase64String(pro.Image5));
                    }
                    return pro;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeletePro(int id)
        {
            try
            {
                var pro = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (pro != null)
                {
                    if (pro.IsActive == false)
                    {
                        pro.IsActive = true;
                        await _dbContext.SaveChangesAsync();
                    }
                    else if (pro.IsActive == true)
                    {
                        pro.IsActive = false;
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task UpdatePro(Product product)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry(product).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

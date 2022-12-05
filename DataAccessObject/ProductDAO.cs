using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<Product>> GetProductAmountSold()
        {
            try
            {
                var lst = await _dbContext.Products
                    .Where(x => x.IsActive == true && x.Amount > 0)
                    .OrderByDescending(x => x.AmountSold).ToListAsync();
                if (lst != null)
                { 
                    return lst.Take(3);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
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
    }
}

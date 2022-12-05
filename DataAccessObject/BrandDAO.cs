using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class BrandDAO
    {
        private static BrandDAO instance;
        private static readonly object instanceLock = new object();
        ComesticDBContext _dbContext = new ComesticDBContext();
        public BrandDAO()
        {

        }
        public static BrandDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BrandDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<IEnumerable<Brand>> GetBrands()
        {
            try
            {
                var lst = await _dbContext.Brands.OrderByDescending(x => x.Id).ToListAsync();
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
        public async Task<Brand> GetBrandById(int id)
        {
            try
            {
                var brand = await _dbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
                if (brand != null)
                {
                    return brand;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteBrand(int id)
        {
            try
            {
                var brand = await _dbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
                if (brand != null)
                {
                    if (brand.IsActive == true)
                    {
                        brand.IsActive = false;
                        await _dbContext.SaveChangesAsync();
                    }
                    else if (brand.IsActive == false)
                    {
                        brand.IsActive = true;
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task AddNewBrand(Brand brand)
        {
            try
            {
                _dbContext.Brands.Add(brand);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateBrand(Brand brand)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry(brand).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

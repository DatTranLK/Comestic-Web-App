using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;
        private static readonly object instanceLock = new object();
        ComesticDBContext _dbContext = new ComesticDBContext();
        public CategoryDAO()
        {

        }
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var lst = await _dbContext.Categories
                    .Include(x => x.Type)
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
        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                var cate = await _dbContext.Categories
                    .Include(x => x.Type)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (cate != null)
                {
                    return cate;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteCategory(int id)
        {
            try
            {
                var cate = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (cate != null)
                {
                    if (cate.IsActive == true)
                    {
                        cate.IsActive = false;
                        await _dbContext.SaveChangesAsync();
                    }
                    else if (cate.IsActive == false)
                    {
                        cate.IsActive = true;
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task AddNewCate(Category category)
        {
            try
            {
                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateCate(Category category)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry(category).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

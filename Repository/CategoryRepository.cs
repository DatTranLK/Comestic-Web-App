using BusinessObject.Models;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task AddNewCate(Category category) => CategoryDAO.Instance.AddNewCate(category);

        public Task DeleteCategory(int id) => CategoryDAO.Instance.DeleteCategory(id);

        public Task<IEnumerable<Category>> GetCategories() => CategoryDAO.Instance.GetCategories();

        public Task<IEnumerable<Category>> GetCategoriesByType(int typeId) => CategoryDAO.Instance.GetCategoriesByType(typeId);

        public Task<IEnumerable<Category>> GetCategoriesVer2() => CategoryDAO.Instance.GetCategoriesVer2();

        public Task<Category> GetCategoryById(int id) => CategoryDAO.Instance.GetCategoryById(id);

        public Task UpdateCate(Category category) => CategoryDAO.Instance.UpdateCate(category);
    }
}

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
        public Task<IEnumerable<Category>> GetCategories() => CategoryDAO.Instance.GetCategories();
    }
}

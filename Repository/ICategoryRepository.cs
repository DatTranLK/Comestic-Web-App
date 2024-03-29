﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Category>> GetCategoriesVer2();
        Task<Category> GetCategoryById(int id);
        Task DeleteCategory(int id);
        Task AddNewCate(Category category);
        Task UpdateCate(Category category);
        Task<IEnumerable<Category>> GetCategoriesByType(int typeId);
    }
}

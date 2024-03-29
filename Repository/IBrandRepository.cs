﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetBrands();
        Task<Brand> GetBrandById(int id);
        Task DeleteBrand(int id);
        Task AddNewBrand(Brand brand);
        Task UpdateBrand(Brand brand);
    }
}

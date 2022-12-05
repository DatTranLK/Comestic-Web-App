using BusinessObject.Models;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BrandRepository : IBrandRepository
    {
        public Task AddNewBrand(Brand brand) => BrandDAO.Instance.AddNewBrand(brand);

        public Task DeleteBrand(int id) => BrandDAO.Instance.DeleteBrand(id);

        public Task<Brand> GetBrandById(int id) => BrandDAO.Instance.GetBrandById(id);

        public Task<IEnumerable<Brand>> GetBrands() => BrandDAO.Instance.GetBrands();

        public Task UpdateBrand(Brand brand) => BrandDAO.Instance.UpdateBrand(brand);
    }
}

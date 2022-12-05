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
    }
}

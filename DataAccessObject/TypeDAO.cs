using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class TypeDAO
    {
        private static TypeDAO instance;
        private static readonly object instanceLock = new object();
        ComesticDBContext _dbContext = new ComesticDBContext();
        public TypeDAO()
        {

        }
        public static TypeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TypeDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<IEnumerable<BusinessObject.Models.Type>> GetTypes()
        {
            try
            {
                var lst = await _dbContext.Types.OrderByDescending(x => x.Id).ToListAsync();
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
        public async Task<IEnumerable<BusinessObject.Models.Type>> GetListTypes()
        {
            try
            {
                var lst = await _dbContext.Types.ToListAsync();
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

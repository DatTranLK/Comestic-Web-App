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
        public async Task<BusinessObject.Models.Type> GetTypeById(int id)
        {
            try
            {
                var type = await _dbContext.Types.FirstOrDefaultAsync(x => x.Id == id);
                if (type != null)
                {
                    return type;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteType(int id)
        {
            try
            {
                var type = await _dbContext.Types.FirstOrDefaultAsync(x => x.Id == id);
                if (type != null)
                {
                    if (type.IsActive == true)
                    {
                        type.IsActive = false;
                        await _dbContext.SaveChangesAsync();
                    }
                    else if (type.IsActive == false)
                    {
                        type.IsActive = true;
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task AddNewType(BusinessObject.Models.Type type)
        {
            try
            {
                _dbContext.Types.Add(type);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateType(BusinessObject.Models.Type type)
        {
            try
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry(type).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

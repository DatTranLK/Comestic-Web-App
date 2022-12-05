using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TypeRepository : ITypeRepository
    {
        public Task AddNewType(BusinessObject.Models.Type type) => TypeDAO.Instance.AddNewType(type);

        public Task DeleteType(int id) => TypeDAO.Instance.DeleteType(id);

        public Task<IEnumerable<BusinessObject.Models.Type>> GetListTypes() => TypeDAO.Instance.GetListTypes();

        public Task<BusinessObject.Models.Type> GetTypeById(int id) => TypeDAO.Instance.GetTypeById(id);

        public Task<IEnumerable<BusinessObject.Models.Type>> GetTypes() => TypeDAO.Instance.GetTypes();

        public Task UpdateType(BusinessObject.Models.Type type) => TypeDAO.Instance.UpdateType(type);
    }
}

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
        public Task<IEnumerable<BusinessObject.Models.Type>> GetListTypes() => TypeDAO.Instance.GetListTypes();

        public Task<IEnumerable<BusinessObject.Models.Type>> GetTypes() => TypeDAO.Instance.GetTypes();
    }
}

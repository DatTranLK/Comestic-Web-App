using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ITypeRepository
    {
        Task<IEnumerable<BusinessObject.Models.Type>> GetTypes();
        Task<IEnumerable<BusinessObject.Models.Type>> GetListTypes();
        Task<BusinessObject.Models.Type> GetTypeById(int id);
        Task DeleteType(int id);
        Task AddNewType(BusinessObject.Models.Type type);
        Task UpdateType(BusinessObject.Models.Type type);
    }
}

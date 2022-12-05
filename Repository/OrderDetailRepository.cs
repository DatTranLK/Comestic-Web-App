using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public Task AddNewOrderDetail(int quantity, int orderId, int productId) => OrderDetailDAO.Instance.AddNewOrderDetail(quantity, orderId, productId);
    }
}

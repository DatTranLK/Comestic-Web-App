using BusinessObject.Models;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        public Task<int> CreateNewOrder(Order order) => OrderDAO.Instance.CreateNewOrder(order);
    }
}

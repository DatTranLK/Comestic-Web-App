using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IOrderRepository
    {
        Task<int> CreateNewOrder(Order order);
        Task<IEnumerable<Order>> GetOrders();
        Task ChangeStatusToAccept(int orderId);
        Task ChangeStatusToCancle(int orderId);
        Task ChangeStatusToDone(int orderId);
        Task<Order> GetOrderById(int id);
    }
}

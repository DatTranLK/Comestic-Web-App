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
        public Task ChangeStatusToAccept(int orderId) => OrderDAO.Instance.ChangeStatusToAccept(orderId);

        public Task ChangeStatusToCancle(int orderId) => OrderDAO.Instance.ChangeStatusToCancle(orderId);

        public Task ChangeStatusToDone(int orderId) => OrderDAO.Instance.ChangeStatusToDone(orderId);

        public Task<int> CreateNewOrder(Order order) => OrderDAO.Instance.CreateNewOrder(order);

        public Task<Order> GetOrderById(int id) => OrderDAO.Instance.GetOrderById(id);

        public Task<IEnumerable<Order>> GetOrders() => OrderDAO.Instance.GetOrders();
    }
}

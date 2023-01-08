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

        public Task<IEnumerable<Order>> GetOrdersAccepted() => OrderDAO.Instance.GetOrdersAccepted();

        public Task<IEnumerable<Order>> GetOrdersAcceptedIn7Days() => OrderDAO.Instance.GetOrdersAcceptedIn7Days();

        public Task<IEnumerable<Order>> GetOrdersByCusId(int cusId) => OrderDAO.Instance.GetOrdersByCusId(cusId);

        public Task<IEnumerable<Order>> GetOrdersCancel() => OrderDAO.Instance.GetOrdersCancel();

        public Task<IEnumerable<Order>> GetOrdersCancelIn7Days() => OrderDAO.Instance.GetOrdersCancelIn7Days();

        public Task<IEnumerable<Order>> GetOrdersDone() => OrderDAO.Instance.GetOrdersDone();

        public Task<IEnumerable<Order>> GetOrdersDoneIn7Days() => OrderDAO.Instance.GetOrdersDoneIn7Days();

        public Task<IEnumerable<Order>> GetOrdersIn7Days() => OrderDAO.Instance.GetOrdersIn7Days();

        public Task<IEnumerable<Order>> GetOrdersProcessing() => OrderDAO.Instance.GetOrdersProcessing();

        public Task<IEnumerable<Order>> GetOrdersProcessingIn7Days() => OrderDAO.Instance.GetOrdersProcessingIn7Days();
    }
}

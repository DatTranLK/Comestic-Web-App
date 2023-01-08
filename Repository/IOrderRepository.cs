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
        Task<IEnumerable<Order>> GetOrdersByCusId(int cusId);
        Task<IEnumerable<Order>> GetOrdersProcessing();
        Task<IEnumerable<Order>> GetOrdersAccepted();
        Task<IEnumerable<Order>> GetOrdersDone();
        Task<IEnumerable<Order>> GetOrdersCancel();
        Task<IEnumerable<Order>> GetOrdersIn7Days();
        Task<IEnumerable<Order>> GetOrdersProcessingIn7Days();
        Task<IEnumerable<Order>> GetOrdersAcceptedIn7Days();
        Task<IEnumerable<Order>> GetOrdersCancelIn7Days();
        Task<IEnumerable<Order>> GetOrdersDoneIn7Days();
    }
}

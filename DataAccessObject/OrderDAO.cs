using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class OrderDAO
    {
        private static OrderDAO instance;
        private static readonly object instanceLock = new object();
        ComesticDBContext _dbContext = new ComesticDBContext();
        public OrderDAO()
        {

        }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<int> CreateNewOrder(Order order)
        {
            try
            {
                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();

                return order.Id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Order> GetOrderById(int id)
        {
            try
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
                if (order != null)
                {
                    return order;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                var orders = await _dbContext.Orders
                    .Include(x => x.Customer)
                    .Include(x => x.Staff)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                if (orders != null)
                {
                    return orders;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task ChangeStatusToAccept(int orderId)
        {
            try
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
                if (order != null && order.OrderStatus == "Processing" || order != null && order.OrderStatus == "Cancle")
                {
                    order.OrderStatus = "Accepted";
                    await _dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task ChangeStatusToCancle(int orderId)
        {
            try
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
                if (order != null && order.OrderStatus == "Processing")
                {
                    order.OrderStatus = "Cancle";
                    await _dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task ChangeStatusToDone(int orderId)
        {
            try
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
                if (order != null && order.OrderStatus == "Accepted")
                {
                    order.OrderStatus = "Done";
                    await _dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Order>> GetOrdersByCusId(int cusId)
        {
            try
            {
                var or = await _dbContext.Orders.Include(x => x.Customer).Include(o => o.Staff).Where(x => x.CustomerId == cusId).OrderByDescending(x => x.Id).ToListAsync();
                if (or != null)
                {
                    return or;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

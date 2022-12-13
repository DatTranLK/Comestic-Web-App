using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance;
        private static readonly object instanceLock = new object();
        ComesticDBContext _dbContext = new ComesticDBContext();
        public OrderDetailDAO()
        {

        }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task AddNewOrderDetail(int quantity, int orderId, int productId)
        {
            try
            {
                var orderDetail = new OrderDetail();
                orderDetail.Quantity = quantity;
                orderDetail.ProductId = productId;
                orderDetail.OrderId = orderId;
                _dbContext.OrderDetails.Add(orderDetail);
                await _dbContext.SaveChangesAsync();
                var book = _dbContext.Products.FirstOrDefault(x => x.Id == productId);
                if (book != null)
                {
                    book.Amount -= quantity;
                    book.AmountSold += quantity;
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IEnumerable<OrderDetail>> GetOrderDetailById(int orderId)
        {
            try
            {
                var od = await _dbContext.OrderDetails
                    .Include(x => x.Product)
                    .Include(x => x.Product.Brand)
                    .Include(x => x.Order)
                    .Where(x => x.OrderId == orderId)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                if (od != null)
                {
                    return od;
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

using BusinessObject.Models;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        public Task<IEnumerable<Product>> GetProductAmountSold() => ProductDAO.Instance.GetProductAmountSold();

        public Task<IEnumerable<Product>> GetProductNewest() => ProductDAO.Instance.GetProductNewest();

        public Task<IEnumerable<Product>> GetProductRecomend() => ProductDAO.Instance.GetProductRecomend();

        public Task<IEnumerable<Product>> GetProducts() => ProductDAO.Instance.GetProducts();
    }
}

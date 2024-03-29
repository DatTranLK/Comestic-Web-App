﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductAmountSold();
        Task<IEnumerable<Product>> GetProductNewest();
        Task<IEnumerable<Product>> GetProductRecomend();
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetListProductAmountSold();
        Task<IEnumerable<Product>> GetListProductNewest();
        Task<IEnumerable<Product>> GetListProductRecomend();
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProductByCategoryId(int cateId);
        Task AddNewProduct(Product product);
        Task<Product> GetProductByproId(int id);
        Task DeletePro(int id);
        Task UpdatePro(Product product);
        Task<IEnumerable<Product>> GetListProductForSaler();
        Task<IEnumerable<Product>> Search(string searchString);
    }
}

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
    }
}
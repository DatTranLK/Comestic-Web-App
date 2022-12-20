using BusinessObject;
using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(Order model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}

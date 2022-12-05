using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountRepository
    {
        Task<Account> CheckLogin(string email, string password);
        Task<Account> GetAccountByEmail(string email);
        Task<Account> Register(string name, string email, string password);
        Task<IEnumerable<Account>> GetAccounts();
    }
}

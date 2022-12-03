using BusinessObject.Models;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        public Task<Account> CheckLogin(string email, string password) => AccountDAO.Instance.CheckLogin(email, password);

        public Task<Account> GetAccountByEmail(string email) => AccountDAO.Instance.GetAccountByEmail(email);
    }
}

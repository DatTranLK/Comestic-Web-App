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
        public Task AddNewAccount(Account account) => AccountDAO.Instance.AddNewAccount(account);

        public Task<Account> CheckLogin(string email, string password) => AccountDAO.Instance.CheckLogin(email, password);

        public Task DeleteAccount(int id) => AccountDAO.Instance.DeleteAccount(id);

        public Task<Account> GetAccountByEmail(string email) => AccountDAO.Instance.GetAccountByEmail(email);

        public Task<Account> GetAccountById(int id) => AccountDAO.Instance.GetAccountById(id);

        public Task<IEnumerable<Account>> GetAccounts() => AccountDAO.Instance.GetAccounts();

        public Task<Account> Register(string name, string email, string password) => AccountDAO.Instance.Register(name, email, password);

        public Task UpdateAccount(Account account) => AccountDAO.Instance.UpdateAccount(account);
    }
}

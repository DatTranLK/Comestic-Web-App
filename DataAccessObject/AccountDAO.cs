using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        private static readonly object instanceLock = new object();
        ComesticDBContext _dbContext = new ComesticDBContext();
        public AccountDAO()
        {

        }
        public static AccountDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<Account> CheckLogin(string email, string password)
        {
            try
            {
                var acc = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(email) && x.Password.Equals(password));
                if (acc != null)
                {
                    return acc;
                }
                return null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Account> GetAccountByEmail(string email)
        {
            try
            {
                var acc = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(email));
                if (acc != null)
                {
                    return acc;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Account> Register(string name, string email, string password)
        {
            try
            {
                var newAcc = new Account();
                newAcc.Name = name;
                newAcc.Email = email;
                newAcc.Password = password;
                newAcc.IsActive = true;
                newAcc.Avatar = "https://static.vecteezy.com/system/resources/previews/003/153/547/original/happy-girl-laughs-human-emotions-avatar-with-happy-woman-vector.jpg";
                newAcc.RoleId = 3;
                _dbContext.Accounts.Add(newAcc);
                await _dbContext.SaveChangesAsync();
                var acc = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(newAcc.Email));
                if (acc != null)
                {
                    return acc;
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

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
                var acc = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(email) && x.Password.Equals(password) && x.IsActive == true);
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
        public async Task<IEnumerable<Account>> GetAccounts()
        {
            try
            {
                var lst = await _dbContext.Accounts
                    .Include(x => x.Role)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();
                if (lst != null)
                {
                    return lst;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<Account> GetAccountById(int id)
        {
            try
            {
                var acc = await _dbContext.Accounts
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task DeleteAccount(int id)
        {
            try
            {
                var acc = await _dbContext.Accounts
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (acc != null)
                {
                    if (acc.IsActive == true)
                    {
                        acc.IsActive = false;
                        await _dbContext.SaveChangesAsync();
                    }
                    else if (acc.IsActive == false)
                    {
                        acc.IsActive = true;
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task AddNewAccount(Account account)
        {
            try
            {
                _dbContext.Accounts.Add(account);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateAccount(Account account)
        {
            try
            {
                /*_dbContext.ChangeTracker.Clear();
                _dbContext.Entry(account).State = EntityState.Modified;*/
                var acc = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == account.Id);
                if (acc != null)
                {
                    acc.Email = account.Email;
                    acc.Password = account.Password;
                    acc.Name = account.Name;
                    acc.DateOfBirth = account.DateOfBirth;
                    acc.RoleId = account.RoleId;
                    acc.Phone = account.Phone;
                    acc.Avatar = "https://static.vecteezy.com/system/resources/previews/003/153/547/original/happy-girl-laughs-human-emotions-avatar-with-happy-woman-vector.jpg";
                    acc.IsActive = true;
                    await _dbContext.SaveChangesAsync();
                }
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

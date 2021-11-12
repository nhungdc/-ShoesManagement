using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        public User CheckLogin(String username, String password)
        {
            User user = null;
            try
            {
                using var context = new ShoeManagementContext();
                user = context.Users.SingleOrDefault(user => (user.Username == username && user.Password == password));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public User GetUserByID(int id)
        {
            User user = null;
            try
            {
                using var context = new ShoeManagementContext();
                user = context.Users.SingleOrDefault(user => user.UserId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
    }
}

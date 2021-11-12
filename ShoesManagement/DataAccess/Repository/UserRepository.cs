using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        public User GetUserByID(int id) => UserDAO.Instance.GetUserByID(id);
        public User CheckLogin(String username, String password) => UserDAO.Instance.CheckLogin(username, password);
    }
}

using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleDAO
    {
        private static RoleDAO instance = null;
        private static readonly object instanceLock = new object();
        public static RoleDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoleDAO();
                    }
                    return instance;
                }
            }
        }

        public Role GetRoleByID(int id)
        {
            Role role = null;
            try
            {
                using var context = new ShoeManagementContext();
                role = context.Roles.SingleOrDefault(role => role.RoleId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return role;
        }
    }
}

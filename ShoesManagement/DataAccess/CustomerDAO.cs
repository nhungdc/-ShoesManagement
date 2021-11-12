using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;
        private static readonly object instanceLock = new object();
        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            List<Customer> cus = null;
            try
            {
                using var context = new ShoeManagementContext();
                cus = context.Customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cus;
        }
        public Customer GetCustomerByID(int id)
        {
            Customer cus = null;
            try
            {
                using var context = new ShoeManagementContext();
                cus = context.Customers.SingleOrDefault(cus => cus.CustomerId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cus;
        }


        public IEnumerable<Customer> GetCustomersByName(string name)
        {
            List<Customer> cus = null;
            try
            {
                using var context = new ShoeManagementContext();
                cus = context.Customers.Where(cus => cus.FullName.ToLower().Contains(name.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cus;
        }

        public IEnumerable<Customer> GetCustomersByIDNumber(string IdNumber)
        {
            List<Customer> cus = null;
            try
            {
                using var context = new ShoeManagementContext();
                cus = context.Customers.Where(cus => cus.Idnumber.ToLower().Contains(IdNumber.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cus;
        }

        public Customer GetCustomerByIDNumber(string IdNumber)
        {
            Customer cus = null;
            try
            {
                using var context = new ShoeManagementContext();
                cus = context.Customers.SingleOrDefault(cus => cus.Idnumber.ToLower().Equals(IdNumber.ToLower()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cus;
        }

        public void AddNew(Customer cus)
        {
            try
            {
                Customer result = GetCustomerByID(cus.CustomerId);
                if (result == null)
                {
                    using var context = new ShoeManagementContext();
                    context.Customers.Add(cus);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This Customer is already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Customer cus)
        {
            try
            {
                Customer result = GetCustomerByID(cus.CustomerId);
                if (result != null)
                {
                    using var context = new ShoeManagementContext();
                    context.Customers.Update(cus);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This Customer does not already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Remove(int id)
        {
            try
            {
                Customer result = GetCustomerByID(id);
                if (result != null)
                {
                    using var context = new ShoeManagementContext();
                    context.Customers.Remove(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This Customer does not already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

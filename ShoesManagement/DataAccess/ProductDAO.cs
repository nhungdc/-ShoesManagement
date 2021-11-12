using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            var list = new List<Product>();
            try
            {
                using var context = new ShoeManagementContext();
                list = context.Products.Where(pro => pro.Status == true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Product GetProductByID(int id)
        {
            Product pro = null;
            try
            {
                using var context = new ShoeManagementContext();
                pro = context.Products.SingleOrDefault(pro => (pro.ProductId == id && pro.Status == true));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pro;
        }

        public Product GetProductByName(String name)
        {
            var pro = new Product();
            try
            {
                using var context = new ShoeManagementContext();
                pro = context.Products.SingleOrDefault(pro => (pro.ProductName.Equals(name) && pro.Status == true));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pro;
        }

        public IEnumerable<Product> GetProductsByName(String name)
        {
            var pro = new List<Product>();
            try
            {
                using var context = new ShoeManagementContext();
                pro = context.Products.Where(pro => (pro.ProductName.ToLower().Contains(name.ToLower()) && pro.Status == true)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pro;
        }

        public void AddNew(Product pro)
        {
            try
            {
                Product result = GetProductByID(pro.ProductId);
                if (result == null)
                {
                    using var context = new ShoeManagementContext();
                    context.Products.Add(pro);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This Product is already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product pro)
        {
            try
            {
                Product result = GetProductByID(pro.ProductId);
                if (result != null)
                {
                    using var context = new ShoeManagementContext();
                    context.Products.Update(pro);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This Product does not already exist");
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
                Product result = GetProductByID(id);
                if (result != null)
                {
                    using var context = new ShoeManagementContext();
                    result.Status = false;
                    context.Products.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This Product does not already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

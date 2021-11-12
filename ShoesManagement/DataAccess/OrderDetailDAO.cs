using BusinessObject;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetailProduct> GetCustomerOrders(int id)
        {
            var result = new List<OrderDetailProduct>();
            try
            {
                using var context = new ShoeManagementContext();
                result = context.OrderDetails.Where(o => (o.OrderId == id && o.Status == true)).Join(
                    context.Products,
                    order => order.ProductId,
                    pro => pro.ProductId,
                    (order, pro) => new OrderDetailProduct
                    {
                        ProductName = pro.ProductName,
                        Quantity = order.Quantity,
                        Price = pro.Price,
                        Total = order.Total,
                    }
                ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public OrderDetail GetByOrderIdAndProductId(int OrderId, int ProductId)
        {
            OrderDetail order = null;
            try
            {
                using var context = new ShoeManagementContext();
                order = context.OrderDetails.SingleOrDefault(or => (or.OrderId == OrderId && or.ProductId == ProductId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public IEnumerable<OrderDetail> GetByOrderId(int OrderId)
        {
            List<OrderDetail> order = null;
            try
            {
                using var context = new ShoeManagementContext();
                order = context.OrderDetails.Where(or => or.OrderId == OrderId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }
        public void AddNew(OrderDetail order)
        {
            try
            {
                using var context = new ShoeManagementContext();
                context.OrderDetails.Add(order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int OrderId, int ProductId)
        {
            try
            {
                OrderDetail result = GetByOrderIdAndProductId(OrderId, ProductId);
                if (result != null)
                {
                    using var context = new ShoeManagementContext();
                    result.Status = false;
                    context.OrderDetails.Update(result);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This Order does not already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(OrderDetail order)
        {
            try
            {
                OrderDetail result = GetByOrderIdAndProductId(order.OrderId, order.ProductId);
                if (result != null)
                {
                    using var context = new ShoeManagementContext();
                    context.OrderDetails.Update(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This Order does not already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

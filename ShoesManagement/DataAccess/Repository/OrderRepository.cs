using BusinessObject;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<CustomerOrder> GetCustomerOrders() => OrderDAO.Instance.GetCustomerOrders();
        public IEnumerable<CustomerOrder> GetCustomerOrdersByName(string name) => OrderDAO.Instance.GetCustomerOrdersByName(name);
        public IEnumerable<CustomerOrder> GetCustomerOrdersById(string id) => OrderDAO.Instance.GetCustomerOrdersByID(id);
        public Order GetOrderByID(int id) => OrderDAO.Instance.GetOrderByID(id);
        public void InsertOrder(Order order) => OrderDAO.Instance.AddNew(order);
        public void UpdateOrder(Order order) => OrderDAO.Instance.Update(order);
        public void DeleteOrder(int id) => OrderDAO.Instance.Remove(id);
    }
}

using BusinessObject;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public IEnumerable<OrderDetailProduct> GetCustomerOrders(int id) => OrderDetailDAO.Instance.GetCustomerOrders(id);
        public OrderDetail GetByOrderIdAndProductId(int OrderId, int ProductId) => OrderDetailDAO.Instance.GetByOrderIdAndProductId(OrderId, ProductId);
        public IEnumerable<OrderDetail> GetByOrderId(int OrderId) => OrderDetailDAO.Instance.GetByOrderId(OrderId);
        public void InsertOrderDetail(OrderDetail order) => OrderDetailDAO.Instance.AddNew(order);
        public void UpdateOrderDetail(OrderDetail order) => OrderDetailDAO.Instance.Update(order);
        public void DeleteOrderDetail(int OrderId, int ProductId) => OrderDetailDAO.Instance.Remove(OrderId, ProductId);
    }
}

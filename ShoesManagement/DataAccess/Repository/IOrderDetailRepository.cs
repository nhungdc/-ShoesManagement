using BusinessObject;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetailProduct> GetCustomerOrders(int id);
        IEnumerable<OrderDetail> GetByOrderId(int OrderId);
        OrderDetail GetByOrderIdAndProductId(int OrderId, int ProductId);
        void InsertOrderDetail(OrderDetail order);
        void UpdateOrderDetail(OrderDetail order);
        void DeleteOrderDetail(int OrderId, int ProductId);
    }
}

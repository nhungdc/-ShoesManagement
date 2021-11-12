using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.DataAccess
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

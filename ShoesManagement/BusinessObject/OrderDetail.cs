using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.DataAccess
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public bool Status { get; set; }

        public void CalculateTotal()
        {
            this.Total = this.Quantity * this.Price;
        }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}

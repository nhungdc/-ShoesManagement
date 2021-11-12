using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class CustomerOrder
    {
        public int OrderId { get; set; }
        public string StaffName { get; set; }
        public string CustomerName { get; set; }
        public string Idnumber { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }

        public CustomerOrder()
        {
        }
    }
}

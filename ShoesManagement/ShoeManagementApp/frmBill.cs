using BusinessObject;
using DataAccess.DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoeManagementApp
{
    public partial class frmBill : Form
    {
        IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        IOrderRepository orderRepository = new OrderRepository();
        IProductRepository productRepository = new ProductRepository();
        public frmBill()
        {
            InitializeComponent();
        }
        public Customer cusInfor;
        public User currentUser { get; set; }
        public List<OrderDetail> currentCart { get; set; }
        public List<OrderDetailProduct> currentDisplay { get; set; }
        public frmOrderProduct oldWindow { get; set; }
        BindingSource orderDetailsSource;

        public void LoadOrderDetailsProductList(IEnumerable<OrderDetailProduct> orderDetailProducts)
        {
            try
            {
                orderDetailsSource = new BindingSource();
                orderDetailsSource.DataSource = orderDetailProducts;

                dgvOrderDetail.DataSource = null;
                dgvOrderDetail.DataSource = orderDetailsSource;
                dgvOrderDetail.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmBill_Load(object sender, EventArgs e)
        {
            LoadOrderDetailsProductList(currentDisplay);
            lbName.Text = cusInfor.FullName;
            lbID.Text = cusInfor.Idnumber;
            lbPhone.Text = cusInfor.Phone;
            decimal total = 0;
            foreach (var item in currentCart)
            {
                total += item.Total;
            }
            txtTotalMoney.Text = total.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("You haven't select payment method", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                decimal total = 0;
                foreach (var item in currentCart)
                {
                    total += item.Total;
                }
                Order order = new Order
                {
                    CustomerId = cusInfor.CustomerId,
                    UserId = currentUser.UserId,
                    Total = total,
                    OrderDate = DateTime.Now,
                    Status = true
                };
                orderRepository.InsertOrder(order);
                foreach (var item in currentCart)
                {
                    item.OrderId = order.OrderId;
                    orderDetailRepository.InsertOrderDetail(item);
                    var product = productRepository.GetProductByID(item.ProductId);
                    product.QuantityInStock -= item.Quantity;
                    productRepository.UpdateProduct(product);
                }
                MessageBox.Show("Sucessful", "Message", MessageBoxButtons.OK);
                frmNewOrder newWindow = new frmNewOrder
                {
                    currentUser = this.currentUser
                };
                newWindow.Show();
                Close();
                oldWindow.Close();
            }
        }
    }
}

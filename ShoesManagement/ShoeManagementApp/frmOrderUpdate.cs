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
    public partial class frmOrderUpdate : Form
    {
        IOrderRepository orderRepository = new OrderRepository();
        IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        IProductRepository productRepository = new ProductRepository();
        BindingSource orderDetailSource;
        public CustomerOrder cusOrderInfor { get; set; }
        public User currentUser { get; set; }
        public frmOrder oldWindow {get; set;}
        public frmOrderUpdate()
        {
            InitializeComponent();
        }
        public void LoadOrderDetailList(IEnumerable<OrderDetailProduct> orders)
        {
            try
            {
                orderDetailSource = new BindingSource();
                orderDetailSource.DataSource = orders;

                txtProductName.DataBindings.Clear();
                txtQuantity.DataBindings.Clear();
                txtPrice.DataBindings.Clear();
                txtTotal2.DataBindings.Clear();

                txtProductName.DataBindings.Add("Text", orderDetailSource, "ProductName");
                txtQuantity.DataBindings.Add("Text", orderDetailSource, "Quantity");
                txtPrice.DataBindings.Add("Text", orderDetailSource, "Price");
                txtTotal2.DataBindings.Add("Text", orderDetailSource, "Total");

                dgvOrderDetail.DataSource = null;
                dgvOrderDetail.DataSource = orderDetailSource;
                dgvOrderDetail.ClearSelection();
                if (orders.Count() == 0)
                {
                    btnRemove.Enabled = false;
                }
                else
                {
                    btnRemove.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void frmOrderUpdate_Load(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You're not allowed to use this", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            else
            {
                txtOrderID.Text = cusOrderInfor.OrderId.ToString();
                txtStaff.Text = cusOrderInfor.StaffName;
                txtCustomer.Text = cusOrderInfor.CustomerName;
                txtIDNumber.Text = cusOrderInfor.Idnumber;
                txtTotal.Text = cusOrderInfor.Total.ToString();
                txtOrderDate.Text = cusOrderInfor.OrderDate.ToString();
                LoadOrderDetailList(orderDetailRepository.GetCustomerOrders(int.Parse(txtOrderID.Text)));
                
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindex = dgvOrderDetail.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvOrderDetail.Rows[selectedrowindex];
                string ProductName = Convert.ToString(selectedRow.Cells["ProductName"].Value);
                Product pro = productRepository.GetProductByName(ProductName);
                OrderDetail result = orderDetailRepository.GetByOrderIdAndProductId(int.Parse(txtOrderID.Text), pro.ProductId);
                pro.QuantityInStock += result.Quantity;
                orderDetailRepository.DeleteOrderDetail(result.OrderId, result.ProductId);
                productRepository.UpdateProduct(pro);
                LoadOrderDetailList(orderDetailRepository.GetCustomerOrders(int.Parse(txtOrderID.Text)));
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("You haven't select any detail", "Minus Error");
            }
        }

        private void refreshOldWindow()
        {
            oldWindow.LoadOrderList(orderRepository.GetCustomerOrders());
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQuantity2.Text))
                {
                    MessageBox.Show("You haven't enter quantity", "Minus Error");
                    return;
                }
                int selectedrowindex = dgvOrderDetail.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvOrderDetail.Rows[selectedrowindex];
                string ProductName = Convert.ToString(selectedRow.Cells["ProductName"].Value);
                Product pro = productRepository.GetProductByName(ProductName);
                OrderDetail result = orderDetailRepository.GetByOrderIdAndProductId(int.Parse(txtOrderID.Text), pro.ProductId);
                Order changeTotal = orderRepository.GetOrderByID(int.Parse(txtOrderID.Text));
                int minus = result.Quantity - int.Parse(txtQuantity2.Text);
                if (minus < 0)
                {
                    MessageBox.Show("Invalid Action!", "Minus Error");
                }
                else
                {
                    pro.QuantityInStock += int.Parse(txtQuantity2.Text);
                    if (minus == 0)
                    {
                        orderDetailRepository.DeleteOrderDetail(result.OrderId, result.ProductId);
                    }
                    else
                    {
                        orderDetailRepository.UpdateOrderDetail(result);
                    }
                    result.Quantity = minus;
                    decimal minusTotal = int.Parse(txtQuantity2.Text) * result.Price;
                    changeTotal.Total -= minusTotal;
                    productRepository.UpdateProduct(pro);
                    orderRepository.UpdateOrder(changeTotal);
                    LoadOrderDetailList(orderDetailRepository.GetCustomerOrders(int.Parse(txtOrderID.Text)));
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You haven't select any detail", "Minus Error");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }
    }
}

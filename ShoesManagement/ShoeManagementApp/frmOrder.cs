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
    public partial class frmOrder : Form
    {
        IOrderRepository orderRepository = new OrderRepository();
        IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        BindingSource orderSource;
        BindingSource orderDetailSource;
        Boolean sort = true;
        public User currentUser { get; set; }
        public frmOrder()
        {
            InitializeComponent();
        }


        private void ClearText()
        {
            txtOrderID.Text = string.Empty;
            txtStaff.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            txtIDNumber.Text = string.Empty;
            txtTotal.Text = string.Empty;
            txtOrderDate.Text = string.Empty;
        }

        public void LoadOrderList(IEnumerable<CustomerOrder> orders)
        {
            try
            {
                orderSource = new BindingSource();
                orderSource.DataSource = orders;

                txtOrderID.DataBindings.Clear();
                txtStaff.DataBindings.Clear();
                txtCustomer.DataBindings.Clear();
                txtIDNumber.DataBindings.Clear();
                txtTotal.DataBindings.Clear();
                txtOrderDate.DataBindings.Clear();


                txtOrderID.DataBindings.Add("Text", orderSource, "OrderId");
                txtStaff.DataBindings.Add("Text", orderSource, "StaffName");
                txtCustomer.DataBindings.Add("Text", orderSource, "CustomerName");
                txtIDNumber.DataBindings.Add("Text", orderSource, "Idnumber");
                txtTotal.DataBindings.Add("Text", orderSource, "Total");
                txtOrderDate.DataBindings.Add("Text", orderSource, "OrderDate");

                dgvOrder.DataSource = null;
                dgvOrder.DataSource = orderSource;
                dgvOrder.ClearSelection();
                dgvOrderDetail.DataSource = null;
                if (orders.Count() == 0)
                {
                    ClearText();
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                    btnUpdate.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }

        public void LoadOrderDetailList(IEnumerable<OrderDetailProduct> orderDetailProducts)
        {
            try
            {
                orderDetailSource = new BindingSource();
                orderDetailSource.DataSource = orderDetailProducts;
                
                dgvOrderDetail.DataSource = orderDetailSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtSearch.Text = String.Empty;
            LoadOrderList(orderRepository.GetCustomerOrders());
            
        }

        private CustomerOrder GetCustomerOrderObject()
        {
            CustomerOrder cusOrder = null;
            try
            {
                cusOrder = new CustomerOrder();
                cusOrder.OrderId = int.Parse(txtOrderID.Text);
                cusOrder.StaffName = txtStaff.Text;
                cusOrder.CustomerName = txtCustomer.Text;
                cusOrder.Idnumber = txtIDNumber.Text;
                cusOrder.OrderDate = DateTime.Parse(txtOrderDate.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Order Error");
            }
            return cusOrder;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var cusOrder = GetCustomerOrderObject();
                if (cusOrder == null)
                {
                    MessageBox.Show("You must select a product first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    orderRepository.DeleteOrder(cusOrder.OrderId);
                    txtSearch.Text = String.Empty;
                    LoadOrderList(orderRepository.GetCustomerOrders());
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You must select a product first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var cusOrder = GetCustomerOrderObject();
                if (cusOrder == null)
                {
                    MessageBox.Show("You must select a product first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var newWindow = new frmOrderUpdate()
                    {
                        cusOrderInfor = GetCustomerOrderObject(),
                        currentUser = this.currentUser,
                        oldWindow = this
                    };
                    if (newWindow.ShowDialog() == DialogResult.OK)
                    {
                        LoadOrderList(orderRepository.GetCustomerOrders());
                        orderSource.Position = orderSource.Count - 1;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You must select a product first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You're not allowed to use this", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtSearch.Text = String.Empty;
            LoadOrderList(orderRepository.GetCustomerOrders());
            
        }

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadOrderDetailList(orderDetailRepository.GetCustomerOrders(int.Parse(txtOrderID.Text)));
            dgvOrderDetail.ClearSelection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            String search = txtSearch.Text.Trim();
            var searchCat = comboSearch.SelectedItem;
            if(!string.IsNullOrEmpty(search) && searchCat == null)
            {
                MessageBox.Show("You must select search category first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if(searchCat != null)
            {
                if (searchCat.ToString().Equals("Name"))
                {
                    LoadOrderList(orderRepository.GetCustomerOrdersByName(search));
                    
                }
                if (searchCat.ToString().Equals("ID"))
                {
                    LoadOrderList(orderRepository.GetCustomerOrdersById(search));
                    
                }
            }
            
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            var list = orderRepository.GetCustomerOrders().OrderBy(order => order.Total).ToList<CustomerOrder>();
            if (sort == false)
            {
                LoadOrderList(list);
                sort = true;
            }
            else
            {
                list.Reverse();
                LoadOrderList(list);
                sort = false;
            }
            
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.currentUser = null;
            this.Hide();
            frmLogin newWindow = new frmLogin();
            newWindow.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmStaff newWindow = new frmStaff
            {
                currentUser = this.currentUser
            };
            this.currentUser = null;
            this.Hide();
            newWindow.Show();
        }
    }
}

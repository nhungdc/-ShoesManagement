using BusinessObject;
using DataAccess.DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ShoeManagementApp
{
    public partial class frmReportByDay : Form
    {
        IOrderRepository orderRepository = new OrderRepository();
        BindingSource orderSource;
        public User currentUser { get; set; }
        public frmReportByDay()
        {
            InitializeComponent();
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.currentUser = null;
            this.Hide();
            frmLogin newWindow = new frmLogin();
            newWindow.Show();
        }

        public void LoadOrderList(IEnumerable<CustomerOrder> orders)
        {
            try
            {

                orderSource = new BindingSource();
                orderSource.DataSource = orders;

                dgvOrder.DataSource = null;
                dgvOrder.DataSource = orderSource;
                dgvOrder.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }

        private void frmStatistic_Load(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You're not allowed to use this", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            RefreshData();
        }

        private void RefreshData()
        {
            comboBox1.DataSource = Enumerable.Range(1, DateTime.Now.Day).Reverse().ToList();
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(DateTime.Now.Day);
            txtTotal.Text = getTotal(getSelectedDayList()).ToString();
            LoadOrderList(getSelectedDayList());
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            frmAdmin newWindow = new frmAdmin
            {
                currentUser = this.currentUser
            };
            this.currentUser = null;
            this.Hide();
            newWindow.Show();
        }
        private IEnumerable<CustomerOrder> getSelectedDayList()
        {
            var selectDay = int.Parse(comboBox1.SelectedItem.ToString());
            var list = orderRepository.GetCustomerOrders().ToList();
            var newList = list.FindAll(item => item.OrderDate.Day == selectDay);
            return newList;
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            var result = getSelectedDayList().OrderBy(item => item.Total);
            txtTotal.Text = getTotal(result).ToString();
            LoadOrderList(result);
        }
        private decimal getTotal(IEnumerable<CustomerOrder> list)
        {
            decimal total = 0;
            foreach (var item in list)
            {
                total += item.Total;
            }
            return total;
        }
        private void btnDesc_Click(object sender, EventArgs e)
        {
            var result = getSelectedDayList().OrderBy(item => item.Total).Reverse();
            txtTotal.Text = getTotal(result).ToString();
            LoadOrderList(result);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTotal.Text = getTotal(getSelectedDayList()).ToString();
            LoadOrderList(getSelectedDayList());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class frmReportByMonth : Form
    {
        IOrderRepository orderRepository = new OrderRepository();
        BindingSource orderSource;
        int? oldStart { get; set; }
        int? oldEnd { get; set; }
        public User currentUser { get; set; }
        public frmReportByMonth()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int start = int.Parse(txtStart.SelectedItem.ToString());
            int end = int.Parse(txtEnd.SelectedItem.ToString());
            if (start > end)
            {
                MessageBox.Show("Start day can't larger than End day", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                oldStart = start;
                oldEnd = end;
                var resultList = getSelectedMonthList().ToList().FindAll(item => (item.OrderDate.Day >= start && item.OrderDate.Day <= end));
                LoadOrderList(resultList);
                txtTotal.Text = getTotal(resultList).ToString();
                LoadOrderList(resultList);
            }
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
            comboBox1.DataSource = Enumerable.Range(1, 12).Reverse().ToList();
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(DateTime.Now.Month);
            txtStart.DataSource = Enumerable.Range(1, 30).Reverse().ToList();
            txtEnd.DataSource = Enumerable.Range(1, 30).Reverse().ToList();
            txtStart.SelectedIndex = txtStart.Items.IndexOf(DateTime.Now.Day);
            txtEnd.SelectedIndex = txtStart.Items.IndexOf(DateTime.Now.Day);
            txtTotal.Text = getTotal(getSelectedMonthList()).ToString();
            LoadOrderList(getSelectedMonthList());
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
        private IEnumerable<CustomerOrder> getSelectedMonthList()
        {
            var selectMonth = int.Parse(comboBox1.SelectedItem.ToString());
            var list = orderRepository.GetCustomerOrders().ToList();
            var newList = list.FindAll(item => (item.OrderDate.Year == DateTime.Now.Year && item.OrderDate.Month == selectMonth));
            return newList;
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtStart.SelectedIndex = txtStart.Items.IndexOf(DateTime.Now.Day);
            txtEnd.SelectedIndex = txtStart.Items.IndexOf(DateTime.Now.Day);
            oldStart = null;
            oldEnd = null;
            RefreshData();
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            if (oldEnd == null && oldStart == null)
            {
                var result = getSelectedMonthList().OrderBy(item => item.Total);
                txtTotal.Text = getTotal(result).ToString();
                LoadOrderList(result);
            }
            else
            {
                var result = getSelectedMonthList()
                    .ToList()
                    .FindAll(item => (item.OrderDate.Day >= oldStart.Value && item.OrderDate.Day <= oldEnd.Value))
                    .OrderBy(item => item.Total);
                txtTotal.Text = getTotal(result).ToString();
                LoadOrderList(result);
            }
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
            if (oldEnd == null && oldStart == null)
            {
                var result = getSelectedMonthList().OrderBy(item => item.Total).Reverse();
                txtTotal.Text = getTotal(result).ToString();
                LoadOrderList(result);

            }
            else
            {
                var result = getSelectedMonthList()
                    .ToList()
                    .FindAll(item => (item.OrderDate.Day >= oldStart.Value && item.OrderDate.Day <= oldEnd.Value))
                    .OrderBy(item => item.Total)
                    .Reverse();
                txtTotal.Text = getTotal(result).ToString();
                LoadOrderList(result);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTotal.Text = getTotal(getSelectedMonthList()).ToString();
            LoadOrderList(getSelectedMonthList());
        }
    }
}

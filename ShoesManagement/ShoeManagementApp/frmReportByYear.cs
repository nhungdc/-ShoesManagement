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
    public partial class frmReportByYear : Form
    {
        IOrderRepository orderRepository = new OrderRepository();
        BindingSource orderSource;
        String oldStart { get; set; }
        String oldEnd { get; set; }
        public User currentUser { get; set; }
        public frmReportByYear()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            DateTime start = startDay.Value;
            DateTime end = endDay.Value;
            Boolean isValidYear = int.TryParse(comboBox1.SelectedItem.ToString(), out int year);
            if (!isValidYear)
            {
                MessageBox.Show("Input Year is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var convertStart = new DateTime(year, start.Month, start.Day);
            var convertEnd = new DateTime(year, end.Month, end.Day);
            if (convertStart > convertEnd)
            {
                MessageBox.Show("Start Day can't be greater than End Day", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                oldStart = convertStart.ToString();
                oldEnd = convertEnd.ToString();
                var resultList = getSelectedYearList().ToList().FindAll(item => (item.OrderDate.Date >= convertStart.Date && item.OrderDate.Date <= convertEnd.Date));
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
            int distance = DateTime.Now.Year - 1950 + 1;
            comboBox1.DataSource = Enumerable.Range(1950, distance).Reverse().ToList();
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(DateTime.Now.Year);
            txtTotal.Text = getTotal(getSelectedYearList()).ToString();
            LoadOrderList(getSelectedYearList());
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
        private IEnumerable<CustomerOrder> getSelectedYearList()
        {
            var selectYear = int.Parse(comboBox1.SelectedItem.ToString());
            var list = orderRepository.GetCustomerOrders().ToList();
            var newList = list.FindAll(item => item.OrderDate.Year == selectYear);
            return newList;
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            startDay.Value = DateTime.Now;
            endDay.Value = DateTime.Now;
            oldStart = null;
            oldEnd = null;
            RefreshData();
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            if (oldEnd == null && oldStart == null)
            {
                var result = getSelectedYearList().OrderBy(item => item.Total);
                txtTotal.Text = getTotal(result).ToString();
                LoadOrderList(result);

            }
            else
            {
                var result = getSelectedYearList()
                    .ToList()
                    .FindAll(item => (item.OrderDate.Date >= DateTime.Parse(oldStart).Date && item.OrderDate.Date <= DateTime.Parse(oldEnd).Date))
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
                var result = getSelectedYearList().OrderBy(item => item.Total).Reverse();
                txtTotal.Text = getTotal(result).ToString();
                LoadOrderList(result);

            }
            else
            {
                var result = getSelectedYearList()
                    .ToList()
                    .FindAll(item => (item.OrderDate.Date >= DateTime.Parse(oldStart).Date && item.OrderDate.Date <= DateTime.Parse(oldEnd).Date))
                    .OrderBy(item => item.Total)
                    .Reverse();
                txtTotal.Text = getTotal(result).ToString();
                LoadOrderList(result);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTotal.Text = getTotal(getSelectedYearList()).ToString();
            LoadOrderList(getSelectedYearList());
        }

       
    }
}

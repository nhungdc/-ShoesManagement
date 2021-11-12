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
    public partial class frmNewOrder : Form
    {
        ICustomerRepository customerRepository = new CustomerRepository();
        public User currentUser { get; set; }
        BindingSource customerSource;
        public frmNewOrder()
        {
            InitializeComponent();
        }

        public void LoadCustomerList(IEnumerable<Customer> customers)
        {
            try
            {
                customerSource = new BindingSource();
                customerSource.DataSource = customers;

                dgvCustomer.DataSource = null;
                dgvCustomer.DataSource = customerSource;
                dgvCustomer.Columns["Orders"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }

        private void frmNewOrder_Load(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You're not allowed to use this", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            LoadCustomerList(customerRepository.GetCustomers());
            dgvCustomer.ClearSelection();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadCustomerList(customerRepository.GetCustomers());
            dgvCustomer.ClearSelection();
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.currentUser = null;
            this.Hide();
            frmLogin newWindow = new frmLogin();
            newWindow.Show();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            String search = txtSearch.Text.Trim();
            var searchCat = comboSearch.SelectedItem;
            if (!string.IsNullOrEmpty(search) && searchCat == null)
            {
                MessageBox.Show("You must select search category first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (searchCat != null)
            {
                if (searchCat.ToString().Equals("Name"))
                {
                    LoadCustomerList(customerRepository.GetCustomersByName(search));
                    dgvCustomer.ClearSelection();

                }
                if (searchCat.ToString().Equals("ID"))
                {
                    LoadCustomerList(customerRepository.GetCustomersByIDNumber(search));
                    dgvCustomer.ClearSelection();
                }
            }
        }

        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            frmOrderNewCustomer newWindow = new frmOrderNewCustomer
            {
                currentUser = this.currentUser,
                oldWindow = this
            };
            newWindow.Show();
        }

        private void btnCreate_Click_1(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindex = dgvCustomer.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvCustomer.Rows[selectedrowindex];
                string CustomerId = Convert.ToString(selectedRow.Cells["CustomerId"].Value);
                string FullName = Convert.ToString(selectedRow.Cells["FullName"].Value);
                string Phone = Convert.ToString(selectedRow.Cells["Phone"].Value);
                string Idnumber = Convert.ToString(selectedRow.Cells["Idnumber"].Value);
                Customer cus = new Customer
                {
                    CustomerId = int.Parse(CustomerId),
                    FullName = FullName,
                    Phone = Phone,
                    Idnumber = Idnumber
                };
                frmOrderProduct newWindow = new frmOrderProduct()
                {
                    customerInfor = cus,
                    currentUser = this.currentUser
                };
                this.currentUser = null;
                this.Hide();
                newWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("You haven't select any customer", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

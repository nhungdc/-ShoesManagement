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
    public partial class frmCustomer : Form
    {
        ICustomerRepository customerRepository = new CustomerRepository();
        public User currentUser { get; set; }
        BindingSource customerSource;
        Boolean sort = true;
        Boolean isSearch = false;
        public frmCustomer()
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
                MessageBox.Show(ex.Message, "Load Customer List");
            }
        }
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You're not allowed to use this", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            LoadCustomerList(customerRepository.GetCustomers());
            dgvCustomer.ClearSelection();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newWindow = new frmCustomerAdd()
            {
                currentUser = this.currentUser,
                oldWindow = this
            };
            if (newWindow.ShowDialog() == DialogResult.OK)
            {
                customerSource.Position = customerSource.Count - 1;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindex = dgvCustomer.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvCustomer.Rows[selectedrowindex];
                string CustomerId = Convert.ToString(selectedRow.Cells["CustomerId"].Value);
                var newWindow = new frmCustomerUpdate()
                {
                    currentUser = this.currentUser,
                    oldWindow = this,
                    cusInfor = customerRepository.GetCustomerByID(int.Parse(CustomerId))
                };
                if (newWindow.ShowDialog() == DialogResult.OK)
                {
                    customerSource.Position = customerSource.Count - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You haven't select any customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            isSearch = false;
            txtSearch.Text = string.Empty;
            LoadCustomerList(customerRepository.GetCustomers());
            dgvCustomer.ClearSelection();
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
                isSearch = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String search = txtSearch.Text.Trim();
            var searchCat = comboSearch.SelectedItem;
            var list = customerRepository.GetCustomers().OrderBy(cus => cus.CustomerId).ToList();
            if (!(!string.IsNullOrEmpty(search) && searchCat != null))
            {
                if (sort == false)
                {
                    LoadCustomerList(list);
                    sort = true;
                }
                else
                {
                    list.Reverse();
                    LoadCustomerList(list);
                    sort = false;
                }
            }
            else 
            {
                if (isSearch)
                {
                    if (searchCat.ToString().Equals("Name"))
                    {
                        var namelist = customerRepository.GetCustomersByName(search).ToList();
                        if (sort == false)
                        {
                            LoadCustomerList(namelist);
                            sort = true;
                        }
                        else
                        {
                            namelist.Reverse();
                            LoadCustomerList(namelist);
                            sort = false;
                        }
                    }
                    if (searchCat.ToString().Equals("ID"))
                    {
                        var idlist = customerRepository.GetCustomersByIDNumber(search).ToList();
                        if (sort == false)
                        {
                            LoadCustomerList(idlist);
                            sort = true;
                        }
                        else
                        {
                            idlist.Reverse();
                            LoadCustomerList(idlist);
                            sort = false;
                        }
                        LoadCustomerList(customerRepository.GetCustomersByIDNumber(search));
                    }
                }
                else
                {
                    if (sort == false)
                    {
                        LoadCustomerList(list);
                        sort = true;
                    }
                    else
                    {
                        list.Reverse();
                        LoadCustomerList(list);
                        sort = false;
                    }
                }
            }
            dgvCustomer.ClearSelection();
        }
    }
}

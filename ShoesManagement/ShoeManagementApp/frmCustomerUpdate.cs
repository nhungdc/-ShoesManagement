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
    public partial class frmCustomerUpdate : Form
    {
        ICustomerRepository customerRepository = new CustomerRepository();
        public User currentUser { get; set; }
        public Customer cusInfor { get; set; }
        public frmCustomer oldWindow { get; set; }
        public frmCustomerUpdate()
        {
            InitializeComponent();
        }

        private Boolean MyValidate()
        {
            Boolean isValid = true;
            String message = "";
            if (String.IsNullOrEmpty(txtFullName.Text.Trim()))
            {
                message += "Your Name is invalid\n";
                isValid = false;
            }
            if (String.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                message += "Your Phone is invalid\n";
                isValid = false;
            }
            if (String.IsNullOrEmpty(txtIDNumber.Text.Trim()))
            {
                message += "ID Number is invalid\n";
                isValid = false;
            }
            else 
            {
                var customer = customerRepository.GetCustomerByIDNumber(txtIDNumber.Text.Trim());
                if (customer != null)
                {
                    if (customer.CustomerId != int.Parse(txtCustomerID.Text))
                    {
                        message += "ID Number is invalid\n";
                        isValid = false;
                    }
                }
            }
            if (!isValid)
            {
                MessageBox.Show(message, "message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return isValid;
        }

        private void RefreshData()
        {
            txtCustomerID.Text = cusInfor.CustomerId.ToString();
            txtFullName.Text = cusInfor.FullName;
            txtIDNumber.Text = cusInfor.Idnumber;
            txtPhone.Text = cusInfor.Phone;
        }

        private void frmCustomerUpdate_Load(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You're not allowed to use this", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            RefreshData();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void label6_MouseHover(object sender, EventArgs e)
        {
            label6.ForeColor = Color.FromArgb(0, 142, 109);
        }

        private void label7_MouseDown(object sender, MouseEventArgs e)
        {
            label7.ForeColor = Color.FromArgb(15, 157, 88);
        }

        private void label6_MouseDown(object sender, MouseEventArgs e)
        {
            label6.ForeColor = Color.FromArgb(15, 157, 88);
        }

        private void label7_MouseHover(object sender, EventArgs e)
        {
            label7.ForeColor = Color.FromArgb(0, 142, 109);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            txtCustomerID.Text = String.Empty;
            txtFullName.Text = String.Empty;
            txtIDNumber.Text = String.Empty;
            txtPhone.Text = String.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (MyValidate())
            {
                try
                {
                    var customer = customerRepository.GetCustomerByID(int.Parse(txtCustomerID.Text));
                    customer.FullName = txtFullName.Text.Trim();
                    customer.Phone = txtPhone.Text.Trim();
                    customer.Idnumber = txtIDNumber.Text.Trim();

                    customerRepository.UpdateCustomer(customer);
                    MessageBox.Show("Update successfully", "Message", MessageBoxButtons.OK);
                    oldWindow.LoadCustomerList(customerRepository.GetCustomers());
                    this.currentUser = null;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

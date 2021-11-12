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
    public partial class frmOrderNewCustomer : Form
    {
        ICustomerRepository customerRepository = new CustomerRepository();
        public User currentUser { get; set; }
        public frmNewOrder oldWindow { get; set; }
        public frmOrderNewCustomer()
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
            else if(customerRepository.GetCustomerByIDNumber(txtIDNumber.Text.Trim()) != null)
            {
                message += "ID Number is invalid\n";
                isValid = false;
            }
            if (!isValid)
            {
                MessageBox.Show(message, "message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return isValid;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (MyValidate())
            {
                try
                {
                    var customer = new Customer
                    {
                        FullName = txtFullName.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Idnumber = txtIDNumber.Text.Trim(),
                    };
                    customerRepository.InsertCustomer(customer);
                    MessageBox.Show("Added successfully", "Message", MessageBoxButtons.OK);
                    oldWindow.currentUser = null;
                    oldWindow.Hide();
                    frmOrderProduct newWindow = new frmOrderProduct
                    {
                        customerInfor = customerRepository.GetCustomerByID(customer.CustomerId),
                        currentUser = this.currentUser
                    };
                    this.currentUser = null;
                    newWindow.Show();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void frmOrderNewCustomer_Load(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You're not allowed to use this", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }
    }
}

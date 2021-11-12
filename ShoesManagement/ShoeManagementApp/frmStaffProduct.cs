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
    public partial class frmStaffProduct : Form
    {
        IProductRepository productRepository = new ProductRepository();
        public User currentUser { get; set; }
        BindingSource productSource;
        public frmStaffProduct()
        {
            InitializeComponent();
        }

        public void LoadProductList(IEnumerable<Product> products)
        {
            try
            {
                productSource = new BindingSource();
                productSource.DataSource = products;
                dgvProduct.DataSource = null;
                dgvProduct.DataSource = productSource;
                dgvProduct.Columns["Status"].Visible = false;
                dgvProduct.Columns["OrderDetails"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }

        private void frmStaffProduct_Load(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You're not allowed to use this", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            LoadProductList(productRepository.GetProducts());
            dgvProduct.ClearSelection();
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
            String search = txtSearch.Text;
            if (String.IsNullOrEmpty(search))
            {
                MessageBox.Show("You must enter a name first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                LoadProductList(productRepository.GetProductsByName(search));
                dgvProduct.ClearSelection();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtSearch.Text = String.Empty;
            LoadProductList(productRepository.GetProducts());
            dgvProduct.ClearSelection();
        }
    }
}

using DataAccess.DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ShoeManagementApp
{
    public partial class frmProduct : Form
    {
        IProductRepository productRepository = new ProductRepository();
        BindingSource productSource;
        Boolean sort = true;
        public User currentUser { get; set; }
        public frmProduct()
        {
            InitializeComponent();
        }

        private void ClearText()
        {
            txtProductID.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtPrice.Text = string.Empty;
        }

        public void LoadProductList(IEnumerable<Product> products)
        {
            try
            {
                productSource = new BindingSource();
                productSource.DataSource = products;

                txtProductID.DataBindings.Clear();
                txtProductName.DataBindings.Clear();
                txtStock.DataBindings.Clear();
                txtPrice.DataBindings.Clear();

                txtProductID.DataBindings.Add("Text", productSource, "ProductId");
                txtProductName.DataBindings.Add("Text", productSource, "ProductName");
                txtStock.DataBindings.Add("Text", productSource, "QuantityInStock");
                txtPrice.DataBindings.Add("Text", productSource, "Price");

                dgvProduct.DataSource = null;
                dgvProduct.DataSource = productSource;
                dgvProduct.Columns["Status"].Visible = false;
                dgvProduct.Columns["OrderDetails"].Visible = false;
                dgvProduct.ClearSelection();

                if (products.Count() == 0)
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

        private Product GetProductObject()
        {
            Product pro = null;
            try
            {
                pro = new Product();
                pro.ProductId = int.Parse(txtProductID.Text);
                pro.ProductName = txtProductName.Text;
                pro.QuantityInStock = int.Parse(txtStock.Text);
                pro.Price = Decimal.Parse(txtPrice.Text);
                pro.Status = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Product Error");
            }
            return pro;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newWindow = new frmProductAdd()
            {
                currentUser = this.currentUser,
                oldWindow = this
            };
            if (newWindow.ShowDialog() == DialogResult.OK)
            {
                LoadProductList(productRepository.GetProducts());
                productSource.Position = productSource.Count - 1;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.currentUser = null;
            this.Hide();
            frmLogin newWindow = new frmLogin();
            newWindow.Show();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You're not allowed to use this", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            LoadProductList(productRepository.GetProducts());
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtSearch.Text = String.Empty;
            LoadProductList(productRepository.GetProducts());
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            var list = productRepository.GetProducts().OrderBy(pro => pro.ProductId).ToList<Product>();
            if (sort == false)
            {
                LoadProductList(list);
                sort = true;
            }
            else
            {
                list.Reverse();
                LoadProductList(list);
                sort = false;
            }
            
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
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var pro = GetProductObject();
                if (pro == null)
                {
                    MessageBox.Show("You must select a product first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    productRepository.DeleteProduct(pro.ProductId);
                    LoadProductList(productRepository.GetProducts());
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
                var pro = GetProductObject();
                if (pro == null)
                {
                    MessageBox.Show("You must select a product first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var newWindow = new frmProductUpdate()
                    {
                        UpdateInfor = GetProductObject(),
                        currentUser = this.currentUser,
                        oldWindow = this
                    };
                    if (newWindow.ShowDialog() == DialogResult.OK)
                    {
                        LoadProductList(productRepository.GetProducts());
                        productSource.Position = productSource.Count - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You must select a product first", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
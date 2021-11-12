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
    public partial class frmOrderProduct : Form
    {
        IProductRepository productRepository = new ProductRepository();
        public Customer customerInfor { get; set; }
        public User currentUser { get; set; }
        BindingSource productSource;
        BindingSource orderDetailsSource;
        List<OrderDetail> currentCart = new List<OrderDetail>();
        List<OrderDetailProduct> currentDisplay = new List<OrderDetailProduct>();
        public frmOrderProduct()
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
        public void LoadOrderDetailsProductList(IEnumerable<OrderDetailProduct> orderDetailProducts)
        {
            try
            {
                orderDetailsSource = new BindingSource();
                orderDetailsSource.DataSource = orderDetailProducts;

                dgvOrderDetail.DataSource = null;
                dgvOrderDetail.DataSource = orderDetailsSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }

        private void frmOrderProduct_Load(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You're not allowed to use this", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            lbCustomerID.Text = customerInfor.Idnumber;
            lbCustomerName.Text = customerInfor.FullName;
            LoadProductList(productRepository.GetProducts());
            dgvProduct.ClearSelection();
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
            frmNewOrder newWindow = new frmNewOrder
            {
                currentUser = this.currentUser
            };
            this.currentUser = null;
            this.Hide();
            newWindow.Show();
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtSearch.Text = String.Empty;
            LoadProductList(productRepository.GetProducts());
            dgvProduct.ClearSelection();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            String addQuantity = txtAdd.Text;
            Boolean addQuanitytCheck = int.TryParse(addQuantity, out int addQuantityAfterCheck);
            txtAdd.Text = String.Empty;
            if (!addQuanitytCheck)
            {
                MessageBox.Show("Invalid add quantity value", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (String.IsNullOrEmpty(addQuantity))
                {
                    MessageBox.Show("Please input your add quantity", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        int selectedrowindex = dgvProduct.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgvProduct.Rows[selectedrowindex];
                        string ProductId = Convert.ToString(selectedRow.Cells["ProductId"].Value); //lay ra product id cua selected row
                        string Price = Convert.ToString(selectedRow.Cells["Price"].Value); //lay ra Price cua selected row
                        string productQuantity = Convert.ToString(selectedRow.Cells["QuantityInStock"].Value); //lay ra Price cua selected row
                        OrderDetail order = new OrderDetail()
                        {
                            ProductId = int.Parse(ProductId),
                            Quantity = addQuantityAfterCheck,
                            Price = decimal.Parse(Price),
                            Status = true
                        };
                        order.CalculateTotal(); //tinh total cua add product

                        string ProductName = Convert.ToString(selectedRow.Cells["ProductName"].Value);
                        OrderDetailProduct details = new OrderDetailProduct() //tao class hien thi ten thay vi id cho order details
                        {
                            ProductName = ProductName,
                            Quantity = addQuantityAfterCheck,
                            Price = decimal.Parse(Price),
                            Total = order.Total
                        };


                        Boolean isFound = false; //cap nhat current cart
                        foreach (var item in currentCart)
                        {
                            if (item.ProductId == order.ProductId && currentDisplay.ElementAt(currentCart.IndexOf(item)) != null)
                            {
                                int addQuantityResult = item.Quantity + order.Quantity;
                                if (addQuantityResult > int.Parse(productQuantity))
                                {
                                    MessageBox.Show("Invalid quantity added, Quantity In Stocks is not Enough", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    item.Quantity = addQuantityResult;
                                    currentDisplay.ElementAt(currentCart.IndexOf(item)).Quantity = addQuantityResult;
                                    item.CalculateTotal(); //update total cua product da co trong cart neu co
                                    currentDisplay.ElementAt(currentCart.IndexOf(item)).Total = item.Total;
                                }
                                isFound = true;
                            }
                        }
                        if (!isFound)
                        {
                            if (int.Parse(productQuantity) < order.Quantity)
                            {
                                MessageBox.Show("Invalid quantity added, Quantity In Stocks is not Enough", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                currentCart.Add(order);
                                currentDisplay.Add(details);
                            }
                        }

                        LoadOrderDetailsProductList(currentDisplay);
                        dgvOrderDetail.ClearSelection();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("You haven't select any item", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            String minusQuantity = txtMinus.Text;
            Boolean addQuanitytCheck = int.TryParse(minusQuantity, out int minusQuantityAfterCheck);
            txtAdd.Text = String.Empty;
            if (!addQuanitytCheck)
            {
                MessageBox.Show("Invalid minus quantity value", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (String.IsNullOrEmpty(minusQuantity))
                {
                    MessageBox.Show("Please input your minus quantity", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        int selectedrowindex = dgvOrderDetail.SelectedCells[0].RowIndex;
                        int selectProductId = currentCart.ElementAt(selectedrowindex).ProductId; //lay ra product id cua selected row

                        Boolean isZero = false; //cap nhat current cart
                        int zeroIndex = -1;
                        foreach (var item in currentCart)
                        {
                            if (item.ProductId == selectProductId && currentDisplay.ElementAt(currentCart.IndexOf(item)) != null)
                            {
                                int minusResult = item.Quantity - minusQuantityAfterCheck;
                                if (minusResult < 0)
                                {
                                    String productNameError = currentDisplay.ElementAt(currentCart.IndexOf(item)).ProductName;
                                    MessageBox.Show("Please check your '" + productNameError + "' quantity, it isn't valid", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else if (minusResult == 0)
                                {
                                    zeroIndex = currentCart.IndexOf(item);
                                    isZero = true;
                                }
                                else
                                {
                                    item.Quantity = minusResult;
                                    currentDisplay.ElementAt(currentCart.IndexOf(item)).Quantity = minusResult;
                                    item.CalculateTotal(); //update total cua product da co trong cart neu co
                                    currentDisplay.ElementAt(currentCart.IndexOf(item)).Total = item.Total;
                                }
                            }
                        }
                        if (isZero)
                        {
                            currentDisplay.RemoveAt(zeroIndex);
                            currentCart.RemoveAt(zeroIndex);
                        }
                        LoadOrderDetailsProductList(currentDisplay);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("You haven't select any order detail item", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            currentCart.Clear();
            currentDisplay.Clear();
            LoadOrderDetailsProductList(currentDisplay);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dgvOrderDetail.SelectedCells[0].RowIndex;
            currentDisplay.RemoveAt(selectedrowindex);
            currentCart.RemoveAt(selectedrowindex);
            LoadOrderDetailsProductList(currentDisplay);
        }
        private void dgvOrderDetail_DataSourceChanged(object sender, EventArgs e)
        {
            if (currentCart.Count != 0)
            {
                decimal total = 0;
                foreach (var item in currentCart)
                {
                    total += item.Total;
                }
                txtTotalMoney.Text = total.ToString();
            }
            else
            {
                txtTotalMoney.Text = string.Empty;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(currentCart.Count == 0)
            {
                MessageBox.Show("No product in order details to make bill", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                frmBill newWindow = new frmBill
                {
                    cusInfor = this.customerInfor,
                    currentUser = this.currentUser,
                    currentCart = this.currentCart,
                    currentDisplay = this.currentDisplay,
                    oldWindow = this
                };
                this.currentUser = null;
                this.Hide();
                newWindow.Show();
            }
        }
    }
}

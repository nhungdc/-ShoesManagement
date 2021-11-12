using DataAccess.DataAccess;
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
    public partial class frmAdmin : Form
    {
        public User currentUser { get; set; }
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You're not allowed to use this", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.currentUser = null;
            this.Hide();
            frmLogin newWindow = new frmLogin();
            newWindow.Show();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmReportByYear newWindow = new frmReportByYear
            {
                currentUser = this.currentUser
            };
            this.currentUser = null;
            newWindow.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmReportByMonth newWindow = new frmReportByMonth
            {
                currentUser = this.currentUser
            };
            this.currentUser = null;
            newWindow.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmReportByDay newWindow = new frmReportByDay
            {
                currentUser = this.currentUser
            };
            this.currentUser = null;
            newWindow.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer.Context;
using Accounting.DataLayer.Services;

namespace Accounting.App
{
    public partial class FrmCustomer : Form
    {
        UnitofWork db = new UnitofWork();
        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        void BindGrid()
        {
            using (UnitofWork db = new UnitofWork())
            {
                dgvCustomer.AutoGenerateColumns = false;
                dgvCustomer.DataSource = db.CustomerRepository.GetAllCustomers();
            }
        }

        private void btnRefreshCustomer_Click(object sender, EventArgs e)
        {
            txtFilterCustomer.Text = null;
            BindGrid();
        }

        private void txtFilterCustomer_TextChanged(object sender, EventArgs e)
        {
            using (UnitofWork db = new UnitofWork())
            {
                dgvCustomer.DataSource = db.CustomerRepository.GetCustomersByFilter(txtFilterCustomer.Text);
            }
        }

        private void btnDeletCustomer_Click(object sender, EventArgs e)
        {
            if(dgvCustomer.CurrentRow != null)
            {
                int customerID = int.Parse(dgvCustomer.CurrentRow.Cells[0].Value.ToString());
                using (UnitofWork db = new UnitofWork())
                {
                    string name = dgvCustomer.CurrentRow.Cells[1].Value.ToString();
                    if (RtlMessageBox.Show($"آیا از حذف '{name}' مطمئنید؟", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        db.CustomerRepository.DeleteCustomer(customerID);
                        db.Save();
                        BindGrid();
                    }
                }
            }
            else
            {
                MessageBox.Show("لطفا یک مشتری را انتخاب کنید");
            }
        }

        private void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            FrmAddEditCustomer frmAdd = new FrmAddEditCustomer();
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomer.CurrentRow != null)
            {
                int customerId = int.Parse(dgvCustomer.CurrentRow.Cells[0].Value.ToString());
                FrmAddEditCustomer frmAddEdit = new FrmAddEditCustomer();
                frmAddEdit.customerId = customerId;
                if (frmAddEdit.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }
            }
        }
    }
}

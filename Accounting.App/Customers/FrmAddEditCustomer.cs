using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using ValidationComponents;

namespace Accounting.App
{
    public partial class FrmAddEditCustomer : Form
    {
        public int customerId = 0;
        UnitofWork db = new UnitofWork();
        public FrmAddEditCustomer()
        {
            InitializeComponent();
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog()== DialogResult.OK)
            { 
                pcCustomer.ImageLocation = openFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string imagename = Guid.NewGuid().ToString()+Path.GetExtension(pcCustomer.ImageLocation);
                string path = Application.StartupPath + "/Images/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pcCustomer.Image.Save(path+imagename);
                Customers custtomer = new Customers()
                {
                    FullName = txtName.Text,
                    Mobile = txtMobile.Text,
                    Email = txtEmail.Text,
                    Address = txtAddress.Text,
                    CustomerImage = imagename
                };
                if (customerId == 0)
                {
                    db.CustomerRepository.InsertCustomer(custtomer);
                }
                else
                {
                    custtomer.CustomerID = customerId;
                    db.CustomerRepository.UpdateCustomer(custtomer);
                }
                    db.Save();
                    DialogResult = DialogResult.OK;
                
                
            }
        }

        private void FrmAddEditCustomer_Load(object sender, EventArgs e)
        {
            if(customerId != 0)
            {
                this.Text = "ویرایش مشتری";
                btnSave.Text = "ویرایش";
                var customer = db.CustomerRepository.GetCustomerById(customerId);
                txtName.Text = customer.FullName;
                txtMobile.Text = customer.Mobile;
                txtEmail.Text = customer.Email;
                txtAddress.Text = customer.Address;
                pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomerImage;
            }
        }
    }
}

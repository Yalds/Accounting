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
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmLogin : Form
    {
        public bool IsEdit = false; 
        public frmLogin()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                using (UnitofWork db = new UnitofWork())
                {
                    if (IsEdit)
                    {
                        var Login = db.LoginRepository.Get().First();
                        Login.UserName = txtUsername.Text;
                        Login.Password = txtPassword.Text;
                        db.LoginRepository.Update(Login);
                        db.Save();
                        Application.Restart();
                    }
                    else
                    {
                        if (db.LoginRepository.Get(l => l.UserName == txtUsername.Text && l.Password == txtPassword.Text).Any())
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            RtlMessageBox.Show("کاربری یافت نشد");
                        }
                    }
                    
                }
            }
                
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if(IsEdit)
            {
                this.Text = "تنظیمات ورود به برنامه";
                btnLogin.Text = "ذخیره";
                using (UnitofWork db = new UnitofWork())
                {
                    var Login = db.LoginRepository.Get().First();
                    txtUsername.Text = Login.UserName;
                    txtPassword.Text = Login.Password;
                }
            }
        }
    }
}

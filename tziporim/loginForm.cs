using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;



namespace tziporim
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void loginForm_Load(object sender, EventArgs e)
        {
              

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            //homeForm hForm = new homeForm("asdf");
            //hForm.Show();
            //this.Hide();
            string name = text_name.Text;
            string password = text_password.Text;
            if (!checkValidAccount(name, password))
            {
                MessageBox.Show("!שם משתמש או סיסמא לא נכונים");
            }
            else
            {
                homeForm hForm = new homeForm(name);
                hForm.Show();
                this.Hide();
            }
        }
        private bool checkValidAccount(string name, string password)
        {
            Excel excel = new Excel(@"database2.xlsx", 1);
            int cellIndex = 1;
            string cell = excel.readCell(cellIndex, 0);
            while (cell != "")
            {
                if (cell == name)
                {
                    string tempPW = excel.readCell(cellIndex, 2);
                    if (tempPW == password)
                    {
                        excel.Close();
                        return true;
                    }
                }
                cell = excel.readCell(++cellIndex, 0);
            }
            excel.Close();
            return false;
        }

        private void regNavBtn_Click(object sender, EventArgs e)
        {
            registerForm regForm = new registerForm();
            regForm.Show();
            this.Hide();
        }
    }
}

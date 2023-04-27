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
      //  Excel excel;
        public loginForm()
        {
           // excel = new Excel(@"C:\Users\Noam\source\repos\tziporim\tziporim\TEST1.xlsx", 1);
            InitializeComponent();
        }

        private void regNavBtn_Click(object sender, EventArgs e)
        {
            registerForm regForm = new registerForm();
            regForm.Show();
            this.Hide();
        }
        

        private void loginForm_Load(object sender, EventArgs e)
        {
           

        }
    }
}

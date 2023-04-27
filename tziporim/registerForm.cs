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
    public partial class registerForm : Form
    {
        public registerForm()
        {
            InitializeComponent();
        }

        private void registerForm_Load(object sender, EventArgs e)
        {
            writeData();
        }

        private void logNavBtn_Click(object sender, EventArgs e)
        {
            loginForm logForm = new loginForm();
            logForm.Show();
            this.Hide();
        }
        public void writeData()
        {
            Excel excel = new Excel(@"Test2.xlsx", 1);

            excel.writeToCell(3, 3, "Test2fsdsffs");
            excel.Save();
            //excel.SaveAs(@"Test2.xlsx");

            excel.Close();
        }
    }
}

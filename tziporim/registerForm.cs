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
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void registerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void logNavBtn_Click(object sender, EventArgs e)
        {
            loginForm logForm = new loginForm();
            logForm.Show();
            this.Hide();
        }
        private int getCount()
        {
            Excel excel = new Excel(@"database2.xlsx", 1);
            string count = excel.readCell(1, 3);
            excel.Close();
            if (count == "")
                return 0;
            else
                return int.Parse(count);
        }
        public void writeData(string Name ,string ID , string password )
        {
            int counter_people = getCount();
            Excel excel = new Excel(@"database2.xlsx", 1);
            
            counter_people++;
            excel.writeToCell(counter_people, 0, Name);
            excel.writeToCell(counter_people, 1, ID);
            excel.writeToCell(counter_people, 2, password);
            excel.writeToCell(1, 3, counter_people.ToString());

            excel.Save();
            //excel.SaveAs(@"database.xlsx");

            excel.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void reg_Btn_Click(object sender, EventArgs e)
        {
            string Name = text_name.Text;
            string ID = text_id.Text;
            string password1 = text_password1.Text;
            string password2 = text_password2.Text;
            if (checkUsername(Name) && checkID(ID) && checkPW(password1, password2))
            {
                MessageBox.Show("!משתמש נוצר בהצלחה");
                writeData(Name, ID, password1);
            }
        }

        private bool checkUsername(string name)
        {
            if (name.Length < 6 || name.Length > 8)
            {
                MessageBox.Show("!האורך של השם משתמש חייב להיות בין 6 ל 8 תווים");
                return false;
            }

            int digCount = 0;
            string noDigName = "";
            foreach(char ch in name)
            {
                if (Char.IsDigit(ch))
                    digCount++;
                else
                    noDigName += ch;
            }
            if(digCount >= 3)
            {
                MessageBox.Show("!השם משתמש חייב להכיל עד 2 ספרות");
                return false;
            }
            if (!noDigName.All(Char.IsLetter))
            {
                MessageBox.Show("!השם משתמש לא יכול להכיל תווים מיוחדים");
                return false;
            }
            else if (!ContainsOnlyEnglishLetters(noDigName))
            {
                MessageBox.Show("!האותיות יכולות להיות רק באנגלית");
                return false;
            }
            return true;
        }
        public static bool ContainsOnlyEnglishLetters(string username)
        {
            string pattern = "^[a-zA-Z]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(username, pattern);
        }
        private bool checkPW(string pw1, string pw2)
        {
            
            if (pw1 != pw2)
            {
                MessageBox.Show("!סיסמאות לא תואמות");
                return false;
            }
            else if(pw1.Length < 8 || pw1.Length > 10)
            {
                MessageBox.Show("!האורך של הסיסמא חייב להיות בין 8 ל 10 תווים");
                return false;
            }
            else if (!pw1.Any(Char.IsLetter))
            {
                MessageBox.Show("!הסיסמא חייבת להכיל לפחות אות אחת");
                return false;
            }
            else if (!pw1.Any(Char.IsDigit))
            {
                MessageBox.Show("!הסיסמא חייבת להכיל לפחות ספרה אחת");
                return false;
            }
            string specialChars = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialCh = specialChars.ToCharArray();

            foreach (char ch in specialCh)
            {
                if (pw1.Contains(ch))
                {
                    return true;
                }
            }
            MessageBox.Show("!הסיסמא חייבת להכיל לפחות תו מיוחד אחד");
            return false;
        }
        private bool checkID(string ID)
        {
            if(ID.Length != 9 || !(ID.All(Char.IsDigit)))
            {
                MessageBox.Show("!המספר ת.ז חייב להיות 9 ספרות בדיוק");
                return false;
            }
            return true;
        }
      
    }
}

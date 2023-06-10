using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tziporim;

namespace tziporim
{
    public partial class cageInfo : Form
    {
        private Cage cage;

        public cageInfo(Cage cage)
        {
            this.cage = cage;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void cageInfo_Load(object sender, EventArgs e)
        {
            showInfo();
            getBirds();
        }

        private void showInfo()
        {
            text_SN.Text = cage.SN;
            text_length.Text = cage.length;
            text_width.Text = cage.width;
            text_height.Text = cage.height;
            text_material.Text = cage.material;
        }
        private void updateCage()
        {

        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int getBirdCount()
        {
            Excel excel = new Excel(@"database2.xlsx", 1);
            string count = excel.readCell(1, 4);
            excel.Close();
            if (count == "")
                return 0;
            else
                return int.Parse(count);
        }
        private int getCageCount()
        {
            Excel excel = new Excel(@"database2.xlsx", 1);
            string count = excel.readCell(1, 5);
            excel.Close();
            if (count == "")
                return 0;
            else
                return int.Parse(count);
        }
        private void getBirds()
        {
            Excel excel = new Excel(@"database2.xlsx", 2);
            int counter_birds = getBirdCount();
            string cageSN = cage.SN;
            var birds = new List<Bird>();
            for (int i = 1; i <= counter_birds; i++)
            {
                if (excel.readCell(i, 5) == cageSN)
                {
                    birds.Add(new Bird(excel.readCell(i, 0), excel.readCell(i, 1), excel.readCell(i, 2), excel.readCell(i, 3), excel.readCell(i, 4), excel.readCell(i, 5), excel.readCell(i, 6), excel.readCell(i, 7)));
                }
            }
            birdsDataGrid.DataSource = birds;
            excel.Close();
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            string SN = textBox_SN.Text;
            string length = textBox_length.Text;
            string width = textBox_width.Text;
            string height = textBox_height.Text;
            string material = comboBox_material.Text;
            int counter_cages = getCageCount();
            Excel excel = new Excel(@"database2.xlsx", 3);
            for(int i = 1; i <= counter_cages; i++)
            {
                if(excel.readCell(i,0) == cage.SN)
                {
                    if(SN != "" && checkCageSN(SN))
                    {
                        excel.writeToCell(i, 0, SN);
                        cage.SN = SN;
                    }
                    if (length != "" && check_cage_details(length))
                    {
                        excel.writeToCell(i, 1, length);
                        cage.length = length;
                    }
                    if (width != "" && check_cage_details(width))
                    {
                        excel.writeToCell(i, 2, width);
                        cage.width = width;
                    }
                    if (height != "" && check_cage_details(height))
                    {
                        excel.writeToCell(i, 3, height);
                        cage.height = height;
                    }
                    if (material != "" && check_material(material))
                    {
                        excel.writeToCell(i, 4, material);
                        cage.material = material;
                    }
                    showInfo();
                    break;
                }
            }
            
            excel.Save();
            excel.Close();
        }
        private bool checkCageSN(string cageSN)
        {
            if (cageSN.Length >= 7 || cageSN.Length < 2)
            {
                MessageBox.Show("המספר סידורי של הכלוב חייב להיות באורך 2 עד 7!");
                return false;
            }
            if (!cageSN.Any(Char.IsDigit))
            {
                MessageBox.Show("המספר סידורי של הכלוב חייב לכלול ספרה");
                return false;
            }
            else if (!cageSN.Any(Char.IsLetter))
            {
                MessageBox.Show("המספר סידורי של הכלוב חייב לכלול אות");
                return false;
            }
            string specialChars = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"" + " ";
            char[] specialCh = specialChars.ToCharArray();

            foreach (char ch in specialCh)
            {
                if (cageSN.Contains(ch))
                {
                    MessageBox.Show("המספר סידורי של הכלוב לא יכול לכלול תווים מיוחדים");
                    return false;
                }
            }
            return true;
        }
        private bool check_material(string material)
        {
            if(material != "עץ" && material != "ברזל" && material != "פלסטיק")
            {
                MessageBox.Show("!חומר הכלוב יכול להיות עץ או ברזל או פלסטיק בלבד");
                return false;
            }
            return true;
        }
        private bool check_cage_details(string para)
        {
            if (!para.All(Char.IsDigit))
            {
                MessageBox.Show("(מדדי הכלוב יכולים לכלול רק מספרים (בסנטימטרים");
                return false;
            }
            else if (int.Parse(para) < 100 || int.Parse(para) > 10000)
            {
                MessageBox.Show("מדדי הכלוב יכולים להיות בין 100 ל 10000 סנטימטרים!");
                return false;
            }
            return true;
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            getBirds();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace tziporim
{
    public partial class homeForm : Form
    {
        private const int MAX_BIRD_AGE = 8;
        private birdInfo birdForm;
        private cageInfo cageForm;

        public homeForm(string username)
        {
            
            InitializeComponent();
            label_greeting.Text += (" " + username);
            this.StartPosition = FormStartPosition.CenterScreen;
            initializeBirthDate();
        }

        private void homeForm_Load(object sender, EventArgs e)
        {

        }
        private void initializeBirthDate()
        {
            text_birthDate.Format = DateTimePickerFormat.Custom;
            text_birthDate.CustomFormat = "dd.MM.yyyy";
            text_birthDate.MaxDate = DateTime.Today;
            text_birthDate.MinDate = DateTime.Today.AddYears(-MAX_BIRD_AGE);
            text_birthDate.Value = DateTime.Today;
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
        private void updateBirdCount(int counter_birds)
        {
            Excel excel = new Excel(@"database2.xlsx", 1);
            excel.writeToCell(1, 4, counter_birds.ToString());
            excel.Save();
            excel.Close();
        }
        private void updateCageCount(int counter_cages)
        {
            Excel excel = new Excel(@"database2.xlsx", 1);
            excel.writeToCell(1, 5, counter_cages.ToString());
            excel.Save();
            excel.Close();
        }
        private void addBird(string species, string subSpecies, string cageSN, string daddySN, string mommySN, string birthDate, string gender)
        {
            int counter_birds = getBirdCount();
            Excel excel = new Excel(@"database2.xlsx", 2);
            counter_birds++;
            excel.writeToCell(counter_birds, 0, (counter_birds).ToString());
            excel.writeToCell(counter_birds, 1, species);
            excel.writeToCell(counter_birds, 2, subSpecies);
            excel.writeToCell(counter_birds, 3, birthDate);
            excel.writeToCell(counter_birds, 4, gender);
            excel.writeToCell(counter_birds, 5, cageSN);
            excel.writeToCell(counter_birds, 6, daddySN);
            excel.writeToCell(counter_birds, 7, mommySN);
            
            excel.Save();
            excel.Close();
            updateBirdCount(counter_birds);
        }
        private void AddNewCage (string SN, string length, string width, string high, string material)
        {
            Excel excel = new Excel(@"database2.xlsx", 3);
            int counter_cages = getCageCount();
            counter_cages++;
            excel.writeToCell(counter_cages, 0, SN);
            excel.writeToCell(counter_cages, 1, length);
            excel.writeToCell(counter_cages, 2, width);
            excel.writeToCell(counter_cages, 3, high);
            excel.writeToCell(counter_cages, 4, material);
           
            excel.Save();
            excel.Close();
            updateCageCount(counter_cages);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string length = this.text_length.Text;
            string width = this.text_width.Text;
            string high = this.text_high.Text;
            string material = this.text_material.Text;
            string SN = this.text_SN.Text;
            if (check_cage_details(length) && check_cage_details(width) && check_cage_details(high)&&check_all(material) && check_all(SN) &&check_cageSN(SN) && check_material(material))
            {
                AddNewCage(SN, length, width, high, material);
                MessageBox.Show("כלוב נוסף בהצלחה");
            }
        }
        private bool check_material(string material)
        {
            if(material != "עץ" && material != "ברזל" && material != "פלסטיק")
            {
                MessageBox.Show("!החומר יכול להיות עץ, ברזל, פלסטיק בלבד");
                return false;
            }
            return true;
        }
        private void addBird_Btn_Click(object sender, EventArgs e)
        {
            string species = this.species.Text;
            string subSpecies = this.subSpecies.Text;
            string cageSN = this.text_cageSN.Text;
            string daddySN = this.text_daddySN.Text;
            string mommySN = this.text_mommySN.Text;
            string gender = this.gender.Text;
            if (check_all(species) && check_all(subSpecies) && checkSpecies(species, subSpecies) && checkCageSN(cageSN) && checkDaddySN(daddySN) && checkMommySN(mommySN) && check_all(text_birthDate.Text) && check_all(gender) && check_gender(gender))
            {
                addBird(species, subSpecies, cageSN, daddySN, mommySN, text_birthDate.Text, gender);
                MessageBox.Show("ציפור נוספה בהצלחה");
            }

        }

        private bool check_gender(string gender)
        {
            if(gender != "זכר" && gender != "נקבה")
            {
                MessageBox.Show("!מין ציפור יכול להיות זכר או נקבה בלבד");
                return false;
            }
            return true;
        }
        private bool check_cageSN(string SN)
        {
            Excel excel = new Excel(@"database2.xlsx", 3);
            int counter_cages = getCageCount();
            
            for(int i = 1; i <= counter_cages; i++)
            {
                if(excel.readCell(i,0) == SN)
                {
                    MessageBox.Show("הכלוב קיים במערכת!");
                    excel.Close();
                    return false;
                }
            }
            excel.Close();
            if (checkCageSN(SN))
                return true;
            else
                return false;
        }
        private bool check_all(string para)
        {
            if (para == "")
            {
                MessageBox.Show("נדרש למלא את כל השדות");
                return false; 
            }
            return true; 
        }
        private bool checkDaddySN(string daddySN)
        {
            if (daddySN.All(Char.IsDigit))
                return true;
            MessageBox.Show("המספר סידורי של האבא יכול להכיל רק ספרות");
            return false;
        }
        private bool checkMommySN(string mommySN)
        {
            if (mommySN.All(Char.IsDigit))
                return true;
            MessageBox.Show("המספר סידורי של האמא יכול להכיל רק ספרות");
            return false;
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
        private bool check_cage_details(string para)
        {
            if (!para.All(Char.IsDigit)){

                MessageBox.Show("מדדי הכלוב יכולים לכלול רק מספרים (בסנטימטרים)");
                return false; 
            }
            else if(int.Parse(para) < 100 || int.Parse(para) > 10000)
            {
                MessageBox.Show("מדדי הכלוב יכולים להיות בין 100 ל 10000 סנטימטרים!");
                return false;
            }
            return true; 
        }
        private bool checkSpecies(string species, string subSpecies)
        {
            if (species != "גולדיאן אמריקאי" && species != "גולדיאן אירופאי" && species != "גולדיאן אוסטרלי")
            {
                MessageBox.Show("!יש לבחור משלושת האפשרויות");
                return false;
            }
            if (subSpecies != "צפון אמריקה" && subSpecies != "מרכז אמריקה" && subSpecies != "דרום אמריקה" && subSpecies != "מזרח אירופה"
                && subSpecies != "מערב אירופה" && subSpecies != "מרכז אוסטרליה" && subSpecies != "ערי חוף")
            {
                MessageBox.Show("!יש לבחור משבעת האפשרויות");
                return false;
            }
            if (species == "גולדיאן אמריקאי")
            {
                if(subSpecies != "צפון אמריקה" && subSpecies != "מרכז אמריקה" && subSpecies != "דרום אמריקה")
                {
                    MessageBox.Show(subSpecies + " הוא לא תת זן של " + species);
                    return false;
                }
                return true;
            }
            else if (species == "גולדיאן אירופאי")
            {
                if (subSpecies != "מזרח אירופה" && subSpecies != "מערב אירופה")
                {
                    MessageBox.Show(subSpecies + " הוא לא תת זן של " + species);
                    return false;
                }
                return true;
            }
            else
            {
                if (subSpecies != "מרכז אוסטרליה" && subSpecies != "ערי חוף")
                {
                    MessageBox.Show(subSpecies + " הוא לא תת זן של " + species);
                    return false;
                }
                return true;
            }
        }
        private int findEmptyCell_birds()
        {
            Excel excel = new Excel(@"database2.xlsx", 2);
            int cellIndex = 1;
            while (true)
            {
                if (excel.readCell(cellIndex, 0) == "")
                {
                    excel.Close();
                    return cellIndex;
                }
                cellIndex++;
            }
        }
        private int findEmptyCell_cages()
        {
            Excel excel = new Excel(@"database2.xlsx", 3);
            int cellIndex = 1;
            while (true)
            {
                if (excel.readCell(cellIndex, 0) == "")
                {
                    excel.Close();
                    return cellIndex;
                }
                cellIndex++;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {
        }

        private void searchBBtn_Click(object sender, EventArgs e)
        {
            string searchBy = combo_bird.Text;
            string searchTxt = text_bird.Text;
            Excel excel = new Excel(@"database2.xlsx", 2);
            var birds = new List<Bird>();
            bool flag = false;
            int counter_birds = getBirdCount();
            if (searchBy == "" || searchTxt == "")
            {
                flag = true;
                MessageBox.Show("נדרש למלא את כל השדות");
            }
            else if (searchBy == "מספר סידורי")
            {
                for(int i = 1; i <= counter_birds; i++)
                {
                    if(excel.readCell(i, 0) == searchTxt)
                    {
                        birds.Add(new Bird(excel.readCell(i, 0), excel.readCell(i, 1), excel.readCell(i, 2), excel.readCell(i, 3), excel.readCell(i, 4), excel.readCell(i, 5), excel.readCell(i, 6), excel.readCell(i, 7)));
                    }
                }
            }
            else if (searchBy == "זן")
            {
                for (int i = 1; i <= counter_birds; i++)
                {
                    if (excel.readCell(i, 1) == searchTxt)
                    {
                        birds.Add(new Bird(excel.readCell(i, 0), excel.readCell(i, 1), excel.readCell(i, 2), excel.readCell(i, 3), excel.readCell(i, 4), excel.readCell(i, 5), excel.readCell(i, 6), excel.readCell(i, 7)));
                    }
                }
            }
            else if (searchBy == "תאריך בקיעה")
            {
                if (!(searchTxt.Length == 10 && searchTxt.Substring(0, 2).All(Char.IsDigit) && searchTxt.Substring(2, 1) == "." && searchTxt.Substring(3, 2).All(Char.IsDigit) && searchTxt.Substring(5,1) == "." && searchTxt.Substring(6, 4).All(Char.IsDigit))){
                    MessageBox.Show("dd.mm.yyyy הכנס תאריך בפורמט");
                    return;
                }
                for (int i = 1; i <= counter_birds; i++)
                {
                    if (excel.readCell(i, 3) == searchTxt)
                    {
                        birds.Add(new Bird(excel.readCell(i, 0), excel.readCell(i, 1), excel.readCell(i, 2), excel.readCell(i, 3), excel.readCell(i, 4), excel.readCell(i, 5), excel.readCell(i, 6), excel.readCell(i, 7)));
                    }
                }
            }
            else if (searchBy == "מין")
            {
                for (int i = 1; i <= counter_birds; i++)
                {
                    if (excel.readCell(i, 4) == searchTxt)
                    {
                        birds.Add(new Bird(excel.readCell(i, 0), excel.readCell(i, 1), excel.readCell(i, 2), excel.readCell(i, 3), excel.readCell(i, 4), excel.readCell(i, 5), excel.readCell(i, 6), excel.readCell(i, 7)));
                    }
                }
            }
            if (birds.Count == 0 && flag == false)
            {
                MessageBox.Show("לא נמצאו תוצאות!");
            }
            birdGridView.DataSource = birds;
            excel.Close();
        }

        private void searchCBtn_Click(object sender, EventArgs e)
        {
            string searchBy = combo_cage.Text;
            string searchTxt = text_cage.Text;
            Excel excel = new Excel(@"database2.xlsx", 3);
            var cages = new List<Cage>();
            bool flag = false;
            int counter_cages = getCageCount();
            if (searchBy == "" || searchTxt == "")
            {
                flag = true;
                MessageBox.Show("נדרש למלא את כל השדות");
            }
            else if (searchBy == "מספר סידורי")
            {
                for (int i = 1; i <= counter_cages; i++)

                {
                
                    if (excel.readCell(i, 0) == searchTxt)
                    {
                        cages.Add(new Cage(excel.readCell(i, 0), excel.readCell(i, 1), excel.readCell(i, 2), excel.readCell(i, 3), excel.readCell(i, 4)));
                    }

                }
            }
            else if (searchBy == "חומר")
            {
                for (int i = 1; i <= counter_cages; i++)
                {
                    if (excel.readCell(i, 4) == searchTxt)
                    {
                        cages.Add(new Cage(excel.readCell(i, 0), excel.readCell(i, 1), excel.readCell(i, 2), excel.readCell(i, 3), excel.readCell(i, 4)));
                    }

                }
            }
            if (cages.Count == 0 && flag == false)
            {
                MessageBox.Show("לא נמצאו תוצאות!");
            }
            cages.Sort(CompareCagesBySerialNumber);
            cageGridView.DataSource = cages;
            excel.Close();
        }
        static int CompareCagesBySerialNumber(Cage c1, Cage c2)
        {
            string x = c1.SN;
            string y = c2.SN;

            int xIndex = 0;
            int yIndex = 0;

            while (xIndex < x.Length && yIndex < y.Length)
            {
                char xChar = x[xIndex];
                char yChar = y[yIndex];

                if (Char.IsDigit(xChar) && Char.IsDigit(yChar))
                {
                    int xNumStart = xIndex;
                    while (xIndex < x.Length && Char.IsDigit(x[xIndex]))
                    {
                        xIndex++;
                    }

                    int yNumStart = yIndex;
                    while (yIndex < y.Length && Char.IsDigit(y[yIndex]))
                    {
                        yIndex++;
                    }

                    string xNumeric = x.Substring(xNumStart, xIndex - xNumStart);
                    string yNumeric = y.Substring(yNumStart, yIndex - yNumStart);
                    int numericComparison = string.Compare(xNumeric, yNumeric, StringComparison.OrdinalIgnoreCase);
                    if (numericComparison != 0)
                    {
                        return numericComparison;
                    }
                }
                else
                {
                    int alphabeticComparison = xChar.CompareTo(yChar);
                    if (alphabeticComparison != 0)
                    {
                        return alphabeticComparison;
                    }

                    xIndex++;
                    yIndex++;
                }
            }

            return x.Length - y.Length;
        }

        private void birdGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is not a header cell
            if (e.RowIndex >= 0)
            {
                // Get the selected object from the DataGridView's DataSource
                var selectedObject = birdGridView.Rows[e.RowIndex].DataBoundItem as Bird;

                // Create a new instance of the customized form and pass the selected object as a parameter
                birdForm = new birdInfo(selectedObject);

                this.Enabled = false;
                birdForm.FormClosed += ChildForm_FormClosed;
                // Show the customized form
                birdForm.Show();
            }
        }
        private void ChildForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Re-enable the main form when the child form is closed
            this.Enabled = true;
        }
        private void cageGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is not a header cell
            if (e.RowIndex >= 0)
            {
                // Get the selected object from the DataGridView's DataSource
                var selectedObject = cageGridView.Rows[e.RowIndex].DataBoundItem as Cage;

                // Create a new instance of the customized form and pass the selected object as a parameter
                cageForm = new cageInfo(selectedObject);

                this.Enabled = false;
                cageForm.FormClosed += ChildForm_FormClosed;
                // Show the customized form
                cageForm.Show();
            }
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            loginForm logForm = new loginForm();
            logForm.Show();
            this.Hide();
        }
    }
}

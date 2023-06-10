using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tziporim
{
    public partial class birdInfo : Form
    {
        private Bird bird;
      
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        public birdInfo(Bird bird)
        {
            player.Stream = tziporim.Properties.Resources.newbird_wav;

            this.bird = bird;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            initializeBirthDate();
        }
        private void initializeBirthDate()
        {
            text_birthDate.Format = DateTimePickerFormat.Custom;
            text_birthDate.CustomFormat = "dd.MM.yyyy";
            text_birthDate.MaxDate = DateTime.Today;
            text_birthDate.MinDate = DateTime.Today.AddMonths(-1);
            text_birthDate.Value = DateTime.Today;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker1.MinDate = DateTime.Today.AddYears(-8);
            dateTimePicker1.Value = DateTime.Today;
        }
        private void birdInfo_Load(object sender, EventArgs e)
        {
            showInfo();
        }
        private void showInfo()
        {
            text_SN.Text = bird.SN;
            text_species.Text = bird.species;
            text_subspecies.Text = bird.subSpecies;
            text_birth_date.Text = bird.birthDate;
            text_gender.Text = bird.gender;
            text_num_cage.Text = bird.cageSN;
            text_num_daddy.Text = bird.daddySN;
            text_num_mommy.Text = bird.mommySN;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addBird_Btn_Click(object sender, EventArgs e)
        {
            
            string species = bird.species;
            string subSpecies = bird.subSpecies;
            string cageSN = bird.cageSN;
            string daddySN;
            string mommySN;
            string parentSN;
            string gender = this.gender.Text;
            if (bird.gender == "זכר")
            {
                daddySN = bird.SN;
                mommySN = text_parentSN.Text;
                parentSN = mommySN;
            }
            else
            {
                mommySN = bird.SN;
                daddySN = text_parentSN.Text;
                parentSN = daddySN;
            }
           
            if (check_all(text_birthDate.Text) && check_all(gender) && checkParent(parentSN) && check_gender(gender))
            {
                player.Play();
                egg_gif.Show();
                addBird(species, subSpecies, cageSN, daddySN, mommySN, text_birthDate.Text, gender);
                MessageBox.Show("גוזל נוסף בהצלחה");
            }
        }
        private bool checkParent(string SN)
        {
            Excel excel = new Excel(@"database2.xlsx", 2);
            int counter_birds = getBirdCount();
            for (int i = 1; i <= counter_birds; i++)
            {
                if(excel.readCell(i,0) == SN)
                {
                    if (excel.readCell(i, 4) != bird.gender)
                    {
                        excel.Close();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("הורים לא יכולים להיות מאותו מין!!");
                    }
                    break;
                }
            }
            excel.Close();
            return false;
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
        private void updateBirdCount(int counter_birds)
        {
            Excel excel = new Excel(@"database2.xlsx", 1);
            excel.writeToCell(1, 4, counter_birds.ToString());
            excel.Save();
            excel.Close();
        }
        private void addBird(string species, string subSpecies, string cageSN, string daddySN, string mommySN, string birthDate, string gender)
        {
            Excel excel = new Excel(@"database2.xlsx", 2);
            int counter_birds = getBirdCount();
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

            delayPics();
        }
        public async void delayPics()
        {
            await Task.Delay(6500);
            bird_pic.Show();
            egg_gif.Hide();
            player.Stop();
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

        private void text_birthDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void editBtmClick_Click(object sender, EventArgs e)
        {
            string species = this.species.Text;
            string subSpecies = this.subSpecies.Text;
            string birthDate = dateTimePicker1.Text;
            string gender = comboBox_gender.Text;
            string num_daddy = textBox_numDaddy.Text;
            string num_cage = textBox_numCage.Text;
            string num_mommy = textBox_numMommy.Text;
            Excel excel = new Excel(@"database2.xlsx", 2);
            int counter_birds = getBirdCount();
            if(species != "")
            {
                if(subSpecies != "")
                {
                    if(checkSpecies(species, subSpecies))
                    {
                        bird.species = species;
                        bird.subSpecies = subSpecies;
                    }
                }
                else
                {
                    if (checkSpecies(species, bird.subSpecies))
                    {
                        bird.species = species;
                    }
                }
            }
            else
            {
                if(subSpecies != "")
                {
                    if (checkSpecies(bird.species, subSpecies))
                    {
                        bird.subSpecies = subSpecies;
                    }
                }
            }
            for (int i = 1; i <= counter_birds; i++)
            {
                if (excel.readCell(i, 0) == bird.SN)
                {
                    excel.writeToCell(i, 1, bird.species);
                    excel.writeToCell(i, 2, bird.subSpecies);

                    if (birthDate != "")
                    {
                        bird.birthDate = birthDate;
                        excel.writeToCell(i, 3, bird.birthDate);
                    }
                    if (gender != "" && check_gender(gender))
                    {
                        bird.gender = gender;
                        excel.writeToCell(i, 4, bird.gender);
                    }
                    if (num_daddy != "")
                    {
                        if (checkDaddySN(num_daddy))
                        {
                            bird.daddySN = num_daddy;
                            excel.writeToCell(i, 6, bird.daddySN);
                        }
                    }
                    if (num_mommy != "")
                    {
                        if (checkMommySN(num_mommy))
                        {
                            bird.mommySN = num_mommy;
                            excel.writeToCell(i, 7, bird.mommySN);
                        }
                    }
                    if (num_cage != "")
                    {
                        if (checkCageSN(num_cage))
                        {
                            bird.cageSN = num_cage;
                            excel.writeToCell(i, 5, bird.cageSN);
                        }
                    }
                    showInfo();
                    break;
                }
            }
            excel.Save();
            excel.Close();
        }
        private bool check_gender(string gender)
        {
            if (gender != "זכר" && gender != "נקבה")
            {
                MessageBox.Show("!מין ציפור יכול להיות זכר או נקבה בלבד");
                return false;
            }
            return true;
        }
        private bool checkCageSN(string cageSN)
        {
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
        private bool checkSpecies(string species, string subSpecies)
        {
            if(species != "גולדיאן אמריקאי" && species != "גולדיאן אירופאי" && species != "גולדיאן אוסטרלי")
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
                if (subSpecies != "צפון אמריקה" && subSpecies != "מרכז אמריקה" && subSpecies != "דרום אמריקה")
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

        private void textBox_numCage_TextChanged(object sender, EventArgs e)
        {

        }
    }
       
    }


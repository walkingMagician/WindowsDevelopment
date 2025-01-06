using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;




namespace Clock
{
    public partial class MainForm : Form
    {
        // -------- JSON ----------//
        private const string SETTINGS_FILE_PATH = "settingsClock.json"; // путь к файлу

        //------- TIME DATA --------//
        private bool isTopMost = false;
        private bool isCheckedData = false;
        private bool isCheckedWeekDay = false;

        //-------- font -------//
        int index = 0;
        string[] strFont = {
            "MOSCOW2024.otf",
            "Ebbe.ttf",
            "Fatal (TRIAL).ttf",
            "micross.ttf"
        };
        Font customFont;
        PrivateFontCollection fontCollection = new PrivateFontCollection();

        //--------------------//
        public MainForm()
        {
            InitializeComponent();

            labelTime.BackColor = Color.AliceBlue;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 50);

            LoadFont(); // загрузка шрифтов 
            
            // синхронизация на есть в автозагрузке или нету 
            toolStripMenuItemLoadOnWindowsStartup.Checked = IsApplicationInStartup(); 
        
        }

        // ----------------------- json -------------------------------//

        // --------------- ---------------------- ----------------------//


        //---------------- working with the registry -------------------//
        private bool IsApplicationInStartup() // проверка статуса автозагрузки
        {
            using (RegistryKey Key = 
                Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"))
            {
                return Key.GetValue("Clock") != null;
            }
        }

        private void AddToStartup() // добавление в атозагрузку
        {
            using (RegistryKey Key =
                Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                Key.SetValue("Clock", "\"" + Application.ExecutablePath + "\"");
            }
        }

        private void RemuveFromStartup() // удаление из автозагрузки 
        {
            using (RegistryKey Key =
                Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (Key.GetValue("Clock") != null)
                    Key.DeleteValue("Clock", false);
            }
        }

        //---------------- --------------------- -------------------//


        void SetVisibility(bool visible)
        {
            checkBoxShowDate.Visible = visible;
            checkBoxShowWeekDay.Visible = visible;
            buttonHideControls.Visible = visible;
            this.FormBorderStyle = visible ? FormBorderStyle.FixedDialog : FormBorderStyle.None;
            this.ShowInTaskbar = visible;
            this.TransparencyKey = visible ? Color.Empty : this.BackColor;
        }

        void LoadFont() // FONT 
        {
            foreach (string strF in strFont)
            {
                try
                {
                    fontCollection.AddFontFile(strF);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading the font from {strF}: {ex.Message}");
                }
            }
            //не знаю почему но он сортирует по имени строки в fontCollection 
        }

        //----------------- Timer Tick -----------------------// 
        private void timer_Tick(object sender, EventArgs e)
        {
            // оброботчик события - эта самая обычная функция, которая неявно вызывается
            //          при возникноввении определенного события 
            // у элемента интерфейса может быть множество событий 
            // и одно из них будет событие по умолчанию 

            //labelTime.Text = DateTime.Now.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            labelTime.Text = DateTime.Now.ToString("HH:mm:ss");
            if (isCheckedData)
                labelTime.Text += $"\n{DateTime.Now.ToString("yyyy.MM.dd")}";
            
            if (isCheckedWeekDay)
                labelTime.Text += $"\n{DateTime.Now.DayOfWeek}";

            // syncing with showData and showWeekDay (false\true)
            checkBoxShowDate.Checked = isCheckedData;
            toolStripMenuItemShowDate.Checked = isCheckedData;

            checkBoxShowWeekDay.Checked = isCheckedWeekDay;
            toolStripMenuItemShowWeekday.Checked = isCheckedWeekDay;

            notifyIcon.Text = $"{DateTime.Now.ToString("HH:mm:ss")}\n" +
                $"{DateTime.Now.ToString("yyyy.MM.dd")}\n" +
                $"{DateTime.Now.DayOfWeek}";
        }

        //------------------------- processing methods ---------------------------//
        private void checkBoxShowDate_CheckedChanged(object sender, EventArgs e)
        {
            isCheckedData = checkBoxShowDate.Checked;
            toolStripMenuItemShowDate.Checked = isCheckedData;
        }
        private void checkBoxShowWeekDay_CheckedChanged(object sender, EventArgs e)
        {
            isCheckedWeekDay = checkBoxShowWeekDay.Checked;
            toolStripMenuItemShowWeekday.Checked = isCheckedData;
        }

        private void buttonHideControls_Click(object sender, EventArgs e)
        {
            SetVisibility(false);
        }

        private void labelTime_DoubleClick(object sender, EventArgs e)
        {
            SetVisibility(true);
        }

        private void toolStripMenuItemTopmost_Click(object sender, EventArgs e)
        {
            isTopMost = !isTopMost;
            this.TopMost = isTopMost;
        }

        private void toolStripMenuItemShowDate_Click(object sender, EventArgs e)
        {

            /*isCheckedData = !isCheckedData;
            checkBoxShowDate.Checked = isCheckedData;
            toolStripMenuItemShowDate.Checked = isCheckedData;*/
        }

        private void toolStripMenuItemShowWeekday_Click(object sender, EventArgs e)
        {
            
            isCheckedWeekDay = !isCheckedWeekDay;
            checkBoxShowWeekDay.Checked = isCheckedWeekDay;
            toolStripMenuItemShowWeekday.Checked = isCheckedWeekDay;

            //toolStripMenuItemShowWeekday.Checked = !toolStripMenuItemShowWeekday.Checked;
            //checkBoxShowWeekDay.Checked = !toolStripMenuItemShowWeekday.Checked;
        }

        private void toolStripMenuItemChooseFont_Click(object sender, EventArgs e)
        {
            //индекс = (индекс + 1) % массив.Length;
            index = (index + 1) % strFont.Length;
            
            customFont = new Font(fontCollection.Families[index], 32);
            labelTime.Font = customFont;
        }
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItemLoadOnWindowsStartup_Click(object sender, EventArgs e)
        {
            
            // проверка на статуст в автозагрузке
            if (IsApplicationInStartup()) 
            { // если есть удалить 
                RemuveFromStartup();
                toolStripMenuItemLoadOnWindowsStartup.Checked = false;
            }
            else
            { // если нету добавить
                AddToStartup();
                toolStripMenuItemLoadOnWindowsStartup.Checked = true;
            }
            
        }

        
        private class Settings
        { 
            public bool IsTopMost { get; set; }
            public bool IsCheckedData { get; set; }
            public bool IsCheckedWeekDay { get; set; }
        }
    }
}


// taskkill -f -im clock.exe
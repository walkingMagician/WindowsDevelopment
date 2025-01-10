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
using System.IO;



namespace Clock
{
    public partial class MainForm : Form
    {
        // -------- directory ----------//
        string Directory; // path directory

        // -------- JSON ----------//
        static string Setting_File_Path; // file path
        private SettingsUser settingsUser; // object from class

        //------- TopMost --------//
        private bool isTopMost = false; 
        // пусть ставить user быть выше всех или нет

        //-------- font -------//
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
            //--------- directory and json -----------//
            Directory = AppDomain.CurrentDomain.BaseDirectory;
            // ---------- --------------- ---------- //
            Setting_File_Path = Path.Combine(Directory, "settingsClock.json"); // путь к файлу
            

            InitializeComponent();

            //labelTime.BackColor = Color.AliceBlue;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 50);

            LoadSettings(); // Json 
            LoadFont(); // loading Font 

            labelTime.ForeColor = settingsUser.ColorLabelFont; // loading the saved color 
            BackColor = settingsUser.ColorBackground; // loading background
            labelTime.BackColor = settingsUser.ColorBackground; // загружаем задний фон labelTime

            // синхронизация на есть в автозагрузке или нету 
            toolStripMenuItemLoadOnWindowsStartup.Checked = IsApplicationInStartup(); 
            
        }

        // ----------------------- json -------------------------------//
        private void LoadSettings() // дисериализация
        {
            settingsUser = LoadSettingsUser(Setting_File_Path);
        }
        private void LoadSaveUser() // Save
        {
            SaveUserData(Setting_File_Path, settingsUser);
            
        }

        private void SaveUserData(string filePath, SettingsUser settingsUser) 
        { // метод создание и сохранение (сереализация)
            try
            {
                string json = JsonConvert.SerializeObject(settingsUser, Formatting.Indented);
                File.WriteAllText(filePath, json);

            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Access error: " + ex.Message);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Input/Output error: " + ex.Message);
            }
            

        }

        private SettingsUser LoadSettingsUser(string filepath)
        { // метод десериализации объекта
            
            try 
            {
                if (!File.Exists(filepath))
                {
                    SettingsUser DefaultUser = new SettingsUser
                    {
                        Data = false,
                        WeekDay = false,
                        Index = 0,
                        ColorLabelFont = Color.Black,
                        ColorBackground = Color.White
                    };

                    LoadSaveUser(); // сохраняем данные по умолчанию
                    return DefaultUser;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"error file not found \n{filepath}: \n{ex.Message}");
            }
            string json = File.ReadAllText(filepath); 
            return JsonConvert.DeserializeObject<SettingsUser>(json); // десериализации 
        }

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


        void LoadFont() // method dynamic donwload 
        {
            foreach (string strF in strFont)
            {
                try
                {
                    string fullPath = Path.Combine(Directory, strF);
                    fontCollection.AddFontFile(fullPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading the font from {strF}: {ex.Message}");
                }
            }
            //не знаю почему но он сортирует по имени строки в fontCollection 
        }

        private void LoadCustomFont(int index) // метод по загрузки шрифта в label
        {
            customFont = new Font(fontCollection.Families[index], 32);
            labelTime.Font = customFont;
        }

        //----------------- Timer Tick -----------------------// 
        private void timer_Tick(object sender, EventArgs e)
        {

            // оброботчик события - эта самая обычная функция, которая неявно вызывается
            //          при возникноввении определенного события 
            // у элемента интерфейса может быть множество событий 
            // и одно из них будет событие по умолчанию 

            //labelTime.Text = DateTime.Now.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            LoadSaveUser(); // так сказать открываем

            labelTime.Text = DateTime.Now.ToString("HH:mm:ss");
            if (settingsUser.Data)
                labelTime.Text += $"\n{DateTime.Now.ToString("yyyy.MM.dd")}";
            
            if (settingsUser.WeekDay)
                labelTime.Text += $"\n{DateTime.Now.DayOfWeek}";

            // syncing with showData and showWeekDay (false\true)
            checkBoxShowDate.Checked = settingsUser.Data;
            toolStripMenuItemShowDate.Checked = settingsUser.Data;

            checkBoxShowWeekDay.Checked = settingsUser.WeekDay;
            toolStripMenuItemShowWeekday.Checked = settingsUser.WeekDay;

            notifyIcon.Text = $"{DateTime.Now.ToString("HH:mm:ss")}\n" +
                $"{DateTime.Now.ToString("yyyy.MM.dd")}\n" +
                $"{DateTime.Now.DayOfWeek}";

            LoadCustomFont(settingsUser.Index);

            LoadSettings(); // а тут закрываем (смутное представление работы)
        }

        //------------------------- processing methods ---------------------------//
        private void checkBoxShowDate_CheckedChanged(object sender, EventArgs e)
        {
            settingsUser.Data = checkBoxShowDate.Checked;
            toolStripMenuItemShowDate.Checked = settingsUser.Data;
        }
        private void checkBoxShowWeekDay_CheckedChanged(object sender, EventArgs e)
        {
            settingsUser.WeekDay = checkBoxShowWeekDay.Checked;
            toolStripMenuItemShowWeekday.Checked = settingsUser.WeekDay;
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

            settingsUser.Data = !settingsUser.Data;
            checkBoxShowDate.Checked = settingsUser.Data;
            toolStripMenuItemShowDate.Checked = settingsUser.Data;
        }

        private void toolStripMenuItemShowWeekday_Click(object sender, EventArgs e)
        {

            settingsUser.WeekDay = !settingsUser.WeekDay;
            checkBoxShowWeekDay.Checked = settingsUser.WeekDay;
            toolStripMenuItemShowWeekday.Checked = settingsUser.WeekDay;

            //toolStripMenuItemShowWeekday.Checked = !toolStripMenuItemShowWeekday.Checked;
            //checkBoxShowWeekDay.Checked = !toolStripMenuItemShowWeekday.Checked;
        }

        private void toolStripMenuItemChooseFont_Click(object sender, EventArgs e)
        {
            //индекс = (индекс + 1) % массив.Length;
            settingsUser.Index = (settingsUser.Index + 1) % strFont.Length;
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

        // ----------------------- COLOR Front ------------------ //
        private void toolStripMenuItemForegroundColor_Click(object sender, EventArgs e)
        { // font color
            // Создание диалогового окна выбора цвета
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Изменение цвета текста метки на выбранный цвет
                    labelTime.ForeColor = colorDialog.Color;
                    settingsUser.ColorLabelFont = colorDialog.Color;
                    SaveUserData(Setting_File_Path, settingsUser);
                }
            }
        }

        private void toolStripMenuItemBackgroundColor_Click(object sender, EventArgs e)
        { // background color
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Изменение цвета текста метки на выбранный цвет
                    BackColor = colorDialog.Color;
                    labelTime.BackColor = colorDialog.Color;
                    settingsUser.ColorBackground = colorDialog.Color;
                    SaveUserData(Setting_File_Path, settingsUser);
                }
            }
        }
    }
    public class SettingsUser // event saving variables class
    {
        public bool Data { get; set; }
        public bool WeekDay { get; set; }
        public int Index { get; set; }
        public Color ColorLabelFont {  get; set; }
        public Color ColorBackground { get; set; }
    }

}


// taskkill -f -im clock.exe
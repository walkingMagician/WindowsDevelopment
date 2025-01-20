using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices; // Dll import
using System.IO; // Directory
using Microsoft.Win32;

namespace Clock
{
    public partial class MainForm : Form
    {
        AlarmClock alarmClock;
        FontDialog fontDialog;
        public MainForm()
        {
            InitializeComponent();
            labelTime.BackColor = Color.AliceBlue;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 50);
            //toolStripMenuItemShowControls.Checked = false;	//Works not correctly
            toolStripMenuItemShowControls.Checked = true;
            toolStripMenuItemShowConsole.Checked = true;


            //fontDialog = new FontDialog();
            //Console.WriteLine(Directory.GetCurrentDirectory());
            //SaveSettings();
            if(File.Exists($"{Path.GetDirectoryName(Application.ExecutablePath)}\\..\\..\\Settings.ini"))
                LoadSettings();
            if (fontDialog == null) fontDialog = new FontDialog();
            if (alarmClock == null) alarmClock = new AlarmClock();

        }

        void SetVisibility(bool visible)
        {
            checkBoxShowDate.Visible = visible;
            checkBoxShowDay.Visible = visible;
            buttonHideControls.Visible = visible;
            this.FormBorderStyle = visible ? FormBorderStyle.FixedDialog : FormBorderStyle.None;
            this.ShowInTaskbar = visible;
            this.TransparencyKey = visible ? Color.Empty : this.BackColor;
        }

        void SaveSettings()
        {
            try
            {
                StreamWriter sw =
                    new StreamWriter($"{Path.GetDirectoryName(Application.ExecutablePath)}\\..\\..\\Settings.ini");
                sw.WriteLine($"{toolStripMenuItemTopmost.Checked}");
                sw.WriteLine($"{toolStripMenuItemShowControls.Checked}");
                sw.WriteLine($"{toolStripMenuItemShowConsole.Checked}");
                sw.WriteLine($"{toolStripMenuItemShowDate.Checked}");
                sw.WriteLine($"{toolStripMenuItemShowDay.Checked}");
                sw.WriteLine($"{fontDialog.FontFileName}");
                sw.WriteLine($"{labelTime.Font.Size}");
                sw.WriteLine($"{labelTime.BackColor.ToArgb()}");
                sw.WriteLine($"{labelTime.ForeColor.ToArgb()}");
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "In SaveSettings()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(this, ex.ToString(), "In SaveSettings()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void LoadSettings()
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader($"{Path.GetDirectoryName(Application.ExecutablePath)}\\..\\..\\Settings.ini");
                toolStripMenuItemTopmost.Checked = Boolean.Parse(sr.ReadLine());
                toolStripMenuItemShowControls.Checked = Boolean.Parse(sr.ReadLine());
                toolStripMenuItemShowConsole.Checked = Boolean.Parse(sr.ReadLine());
                toolStripMenuItemShowDate.Checked = Boolean.Parse(sr.ReadLine());
                toolStripMenuItemShowDay.Checked = Boolean.Parse(sr.ReadLine());
                string fontname = sr.ReadLine();
                float fontsize = (float)Convert.ToDouble(sr.ReadLine());
                labelTime.BackColor = Color.FromArgb(Convert.ToInt32(sr.ReadLine()));
                labelTime.ForeColor = Color.FromArgb(Convert.ToInt32(sr.ReadLine()));
                sr.Close();
                fontDialog = new FontDialog(fontname, fontsize);
                labelTime.Font = fontDialog.Font;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "In LoadSettings()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(this, ex.ToString(), "In LoadSettings()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // оброботчик события - эта самая обычная функция, которая неявно вызывается
            //          при возникноввении определенного события 
            // у элемента интерфейса может быть множество событий 
            // и одно из них будет событие по умолчанию 

            //labelTime.Text = DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
            labelTime.Text = DateTime.Now.ToString("HH:mm:ss");
            if (checkBoxShowDate.Checked)
                labelTime.Text += $"\n{DateTime.Now.ToString("yyyy.MM.dd")}";
            if (checkBoxShowDay.Checked)
                labelTime.Text += $"\n{DateTime.Now.DayOfWeek}";
            
            notifyIcon.Text = $"{DateTime.Now.ToString("HH:mm:ss")}\n" +
                $"{DateTime.Now.ToString("yyyy.MM.dd")}\n" +
                $"{DateTime.Now.DayOfWeek}";            

            SaveSettings();
            labelTime.Font = fontDialog.Font;
        }

        private void buttonHideControls_Click(object sender, EventArgs e)
        {
            //SetVisibility(false);
            toolStripMenuItemShowControls.Checked = false;
        }

        private void labelTime_DoubleClick(object sender, EventArgs e) 
        {
            //SetVisibility(true); 
            toolStripMenuItemShowControls.Checked = true;
        }
        

        private void toolStripMenuItemExit_Click(object sender, EventArgs e) =>
            this.Close();

        private void toolStripMenuItemTopmost_CheckedChanged(object sender, EventArgs e) =>
            this.TopMost = toolStripMenuItemTopmost.Checked;

        private void toolStripMenuItemShowControls_CheckStateChanged(object sender, EventArgs e) =>
            SetVisibility(toolStripMenuItemShowControls.Checked);
        
        private void toolStripMenuItemShowDate_CheckedChanged(object sender, EventArgs e) =>
            checkBoxShowDate.Checked = toolStripMenuItemShowDate.Checked;
        
        private void checkBoxShowDate_CheckedChanged(object sender, EventArgs e) =>
            toolStripMenuItemShowDate.Checked = checkBoxShowDate.Checked;

        private void checkBoxShowDay_CheckedChanged(object sender, EventArgs e) =>
            toolStripMenuItemShowDay.Checked = checkBoxShowDay.Checked;

        private void toolStripMenuItemShowDay_CheckedChanged(object sender, EventArgs e) =>
            checkBoxShowDay.Checked = toolStripMenuItemShowDay.Checked;


        private void toolStripMenuItemBackgroundColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = labelTime.BackColor;
           DialogResult result =  colorDialog.ShowDialog(this);
            if (result == DialogResult.OK)
                labelTime.BackColor = colorDialog.Color;
        }

        private void toolStripMenuItemForegroundColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = labelTime.ForeColor;
            if(colorDialog.ShowDialog(this) == DialogResult.OK) 
                labelTime.ForeColor = colorDialog.Color;
        }

        private void toolStripMenuItemChooseFont_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog(this) == DialogResult.OK)
            {
                labelTime.Font = fontDialog.Font;
            }
        }
        private void toolStripMenuItemaAlarmClock_Click(object sender, EventArgs e)
        {
            alarmClock.ShowDialog();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e) // NotifyIcon
        {
            if (!this.TopMost)
            {
                this.TopMost = true;
                this.TopMost = false;
            }
        }
        private void toolStripMenuItemLoadOnWindowsStartup_CheckedChanged(object sender, EventArgs e)
        {
            string key_name = "Clock";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(
                "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true); // true - writable
            if (toolStripMenuItemLoadOnWindowsStartup.Checked) key.SetValue(key_name, Application.ExecutablePath);
            else key.DeleteValue(key_name, false); // false - throwOnMissinggValue (бросить если удаляемое значение отсутствует)
            key.Dispose();
        }
        

        // -------------------- //
        private void toolStripMenuItemShowConsole_CheckedChanged(object sender, EventArgs e)
        {
            AllocConsole();
            bool show = toolStripMenuItemShowConsole.Checked ? AllocConsole() : FreeConsole();
        }
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }


        // ---- //
    }
}

// taskkill -f -im clock.exe
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

namespace Clock
{
    public partial class MainForm : Form
    {
        FontDialog fontDialog;
        public MainForm()
        {
            InitializeComponent();
            labelTime.BackColor = Color.AliceBlue;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 50);
            //toolStripMenuItemShowControls.Checked = false;
            toolStripMenuItemShowControls.Checked = true;
            toolStripMenuItemShowConsole.Checked = true;
            
            fontDialog = new FontDialog();


        }

        void SetVisibility(bool visible)
        {
            checkBoxShowDate.Visible = visible;
            checkBoxShowWeekday.Visible = visible;
            buttonHideControls.Visible = visible;
            this.FormBorderStyle = visible ? FormBorderStyle.FixedDialog : FormBorderStyle.None;
            this.ShowInTaskbar = visible;
            this.TransparencyKey = visible ? Color.Empty : this.BackColor;
        }


        void LoadSettings()
        {
            StreamReader sr =
                new StreamReader($"{Path.GetDirectoryName(Application.ExecutablePath)}\\..\\..\\Settings.ini");
            toolStripMenuItemTopmost.Checked = Boolean.Parse( sr.ReadLine() );
            toolStripMenuItemShowControls.Checked = Boolean.Parse( sr.ReadLine() );
            toolStripMenuItemShowConsole.Checked = Boolean.Parse( sr.ReadLine() );
            toolStripMenuItemShowDate.Checked = Boolean.Parse( sr.ReadLine());
            toolStripMenuItemShowWeekday.Checked = Boolean.Parse( sr.ReadLine());
            string fontName = sr.ReadLine();
            double fontsize = Convert.ToDouble( sr.ReadLine() );
            labelTime.BackColor = Color.FromArgb(Convert.ToInt32(sr.ReadLine()));
            labelTime.ForeColor = Color.FromArgb(Convert.ToInt32(sr.ReadLine()));

            sr.Close();
        }

        void SaveSettings()
        {
            StreamWriter sw = 
                new StreamWriter($"{Path.GetDirectoryName(Application.ExecutablePath)}\\..\\..\\Settings.ini");
            sw.WriteLine($"{toolStripMenuItemTopmost.Checked}");
            sw.WriteLine($"{toolStripMenuItemShowControls.Checked}");
            sw.WriteLine($"{toolStripMenuItemShowConsole.Checked}");
            sw.WriteLine($"{toolStripMenuItemShowDate.Checked}");
            sw.WriteLine($"{toolStripMenuItemShowWeekday.Checked}");
            sw.WriteLine($"{labelTime.Font.Name}");
            sw.WriteLine($"{labelTime.Font.Size}");
            sw.WriteLine($"{labelTime.BackColor.ToArgb()}");
            sw.WriteLine($"{labelTime.ForeColor.ToArgb()}");
            sw.Close();
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
            if (checkBoxShowWeekday.Checked)
                labelTime.Text += $"\n{DateTime.Now.DayOfWeek}";
            
            notifyIcon.Text = $"{DateTime.Now.ToString("HH:mm:ss")}\n" +
                $"{DateTime.Now.ToString("yyyy.MM.dd")}\n" +
                $"{DateTime.Now.DayOfWeek}";
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

        private void toolStripMenuItemShowControls_CheckedChanged(object sender, EventArgs e) =>
            SetVisibility(toolStripMenuItemShowControls.Checked);
        

        private void toolStripMenuItemShowDate_CheckedChanged(object sender, EventArgs e) =>
            checkBoxShowDate.Checked = toolStripMenuItemShowDate.Checked;
        

        private void checkBoxShowDate_CheckedChanged(object sender, EventArgs e) =>
            toolStripMenuItemShowDate.Checked = checkBoxShowDate.Checked;

        private void toolStripMenuItemShowWeekday_CheckedChanged(object sender, EventArgs e) =>
            checkBoxShowWeekday.Checked = toolStripMenuItemShowWeekday.Checked;
        private void checkBoxShowWeekday_CheckedChanged(object sender, EventArgs e) =>
            toolStripMenuItemShowWeekday.Checked = checkBoxShowWeekday.Checked;

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
                labelTime.Font = fontDialog.font; 
           }
            
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (!this.TopMost)
            {
                this.TopMost = true;
                this.TopMost = false;
            }
        }

        private void toolStripMenuItemShowConsole_CheckedChanged(object sender, EventArgs e)
        {
            //AllocConsole();
            bool show = toolStripMenuItemShowConsole.Checked ? AllocConsole() : FreeConsole();
        }
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();
        



        // ---- //
    }
}

// taskkill -f -im clock.exe
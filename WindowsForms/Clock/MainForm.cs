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
using System.Drawing.Text;

namespace Clock
{
    public partial class MainForm : Form
    {
        private bool isTopMost = false;

        string[] strFont = {
            "MOSCOW2024.otf",
            "Ebbe.ttf",
            "Fatal (TRIAL).ttf",
            "micross.ttf"
        };
        int index = 0;
        Font customFont;
        PrivateFontCollection fontCollection = new PrivateFontCollection();

        public MainForm()
        {
            InitializeComponent();

            labelTime.BackColor = Color.AliceBlue;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 50);

            LoadFont();
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

        void LoadFont()
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

        private void timer_Tick(object sender, EventArgs e)
        {
            // оброботчик события - эта самая обычная функция, которая неявно вызывается
            //          при возникноввении определенного события 
            // у элемента интерфейса может быть множество событий 
            // и одно из них будет событие по умолчанию 

            //labelTime.Text = DateTime.Now.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            labelTime.Text = DateTime.Now.ToString("HH:mm:ss");
            if (checkBoxShowDate.Checked)
            {
                labelTime.Text += $"\n{DateTime.Now.ToString("yyyy.MM.dd")}";
            }
            if (checkBoxShowWeekday.Checked)
                labelTime.Text += $"\n{DateTime.Now.DayOfWeek}";
            toolStripMenuItemShowDate.Checked = checkBoxShowDate.Checked;
            toolStripMenuItemShowWeekday.Checked = checkBoxShowWeekday.Checked;

            notifyIcon.Text = $"{DateTime.Now.ToString("HH:mm:ss")}\n" +
                $"{DateTime.Now.ToString("yyyy.MM.dd")}\n" +
                $"{DateTime.Now.DayOfWeek}";
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
            toolStripMenuItemShowDate.Checked = !toolStripMenuItemShowDate.Checked;
            checkBoxShowDate.Checked = !toolStripMenuItemShowDate.Checked;

        }

        private void toolStripMenuItemShowWeekday_Click(object sender, EventArgs e)
        {
            toolStripMenuItemShowWeekday.Checked = !toolStripMenuItemShowWeekday.Checked;
            checkBoxShowWeekday.Checked = !toolStripMenuItemShowWeekday.Checked;
        }

        private void toolStripMenuItemChooseFont_Click(object sender, EventArgs e)
        {
            //индекс = (индекс + 1) % массив.Length;
            index = (index + 1) % strFont.Length;
            
            customFont = new Font(fontCollection.Families[index], 32);
            labelTime.Font = customFont;
        }
    }
}

// taskkill -f -im clock.exe
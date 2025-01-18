using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace Clock
{
    public partial class FontDialog : Form
    {
        // -- parametrs -- //
        string execute_Path = ""; 
        string fonts_Path = "";
        public string FontsPath { get => fonts_Path; }
        public string FontFileName { get; set; }
        public Font Font {  get; set; }
        
        // -------------------- //
        public FontDialog()
        {
            InitializeComponent();

            execute_Path = Path.GetDirectoryName(Application.ExecutablePath);
            fonts_Path = $"{execute_Path}\\..\\..\\Fonts";
            LoadFonts();
            Font = labelExample.Font;
        }
        public FontDialog(string fontName, float fontSize) : this()
        {
            numericUpDownFontSize.Value = (decimal)fontSize;
            comboBoxFonts.SelectedIndex = comboBoxFonts.Items.IndexOf(fontName);
            SetFont();
            Font = labelExample.Font;
            FontFileName = fontName;
        }



        void LoadFonts()
        { 
            
            //Directory.SetCurrentDirectory(fonts_Path);
            //Console.WriteLine(Directory.GetCurrentDirectory());

            comboBoxFonts.Items.AddRange(GetFontsFromDirectory(fonts_Path, "*.ttf"));
            comboBoxFonts.Items.AddRange(GetFontsFromDirectory(fonts_Path, "*.otf"));
            comboBoxFonts.SelectedIndex = 0;
        }

        string[] GetFontsFromDirectory(string directory, string format)
        {
            string[] fonts = Directory.GetFiles(directory, format);
            for (int i = 0; i < fonts.Length; i++)
            {
                fonts[i] = fonts[i].Split('\\').Last();
            }
            return fonts;
        }

        void SetFont()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile($"{fonts_Path}\\{comboBoxFonts.SelectedItem}");
            labelExample.Font = new Font(pfc.Families[0], Convert.ToInt32(numericUpDownFontSize.Value));
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFont();
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            Font = labelExample.Font;
            FontFileName = comboBoxFonts.SelectedItem.ToString();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            SetFont();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

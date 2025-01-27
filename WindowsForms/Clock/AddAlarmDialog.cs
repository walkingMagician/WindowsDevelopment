using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;     


namespace Clock
{
    public partial class AddAlarmDialog : Form
    {

        public Alarm Alarm;
        OpenFileDialog openFile;


        public AddAlarmDialog()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            Alarm = new Alarm();
            SetWeekDays();
            openFile = new OpenFileDialog();
        }

        void SetWeekDays()
        {
            bool[] days = Alarm.Week.ToArray();
            for (int i = 0; i < checkedListBoxWeekDay.Items.Count; i++)
            {
                checkedListBoxWeekDay.SetItemChecked(i, days[i]);
            }
        }
        private void checkBoxUseDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerDate.Enabled = checkBoxUseDate.Checked;
            checkedListBoxWeekDay.Enabled = !checkBoxUseDate.Checked;
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Alarm.Date = dateTimePickerDate.Enabled ? dateTimePickerTime.Value : DateTime.MinValue;
            Alarm.Time = dateTimePickerTime.Value.TimeOfDay;
            Alarm.Week = new Week
                (
                checkedListBoxWeekDay.
                Items.
                Cast<object>().
                Select((item, index) => checkedListBoxWeekDay.GetItemChecked(index)).ToArray()
                );

            if (labelFileName.Text != "Filename" && labelFileName.Text != "")
            {
                Alarm.Filename = openFile.FileName;
            }
            else
            {
                MessageBox.Show(this, "Выберите звуковой файл", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.None;
            }
            Alarm.Message = richTextBoxMessage.Text;
            
            /*dateTime = dateTimePickerDate.Value.Date;
            TimeSpan timeSpan = dateTimePickerTime.Value.TimeOfDay;
            dateTime = dateTime.Add(timeSpan);*/

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void ShowIndexes()
        {
            for (int i = 0; i < checkedListBoxWeekDay.Items.Count; i++)
            {
                string item = checkedListBoxWeekDay.Items[i].ToString();
                Console.Write($"Элемент: {item}, Индекс: {i}\t");
                //Console.Write(checkedListBoxWeekDay.GetItemChecked(i) + "\t");
            }
            Console.WriteLine();
        }

        private void buttonChooseFile_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            { 
                labelFileName.Text = $"Filename: {openFile.FileName}";
            }
        }


    }
}

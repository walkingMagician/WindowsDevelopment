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
        private Week week = new Week();
        private Alarm alarm = new Alarm();
        private AlarmForm alarmForm;
        public DateTime dateTime { get; private set; }
        public bool useDate { get; private set; }
        public byte selectedDays { get; private set; }
        public AddAlarmDialog()
        {
            InitializeComponent();

        }

        private void checkBoxUseDate_CheckedChanged(object sender, EventArgs e)
        {
            useDate = checkBoxUseDate.Checked;
            dateTimePickerDate.Enabled = useDate;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            

            dateTime = dateTimePickerDate.Value.Date;
            TimeSpan timeSpan = dateTimePickerTime.Value.TimeOfDay;
            dateTime = dateTime.Add(timeSpan);
  
            this.DialogResult = DialogResult.OK;
            this.Close();
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

    }
}

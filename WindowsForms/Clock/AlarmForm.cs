using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class AlarmForm : Form
    {
        private AddAlarmDialog addAlarmDialog;
        private Week week = new Week();
        private Alarm alarm = new Alarm();

        public AlarmForm()
        {
            InitializeComponent();
            if (addAlarmDialog == null) addAlarmDialog = new AddAlarmDialog();
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (addAlarmDialog.ShowDialog() == DialogResult.OK)
            {
                
                    

                alarm.Date = addAlarmDialog.dateTime;
                bool useDate = addAlarmDialog.useDate;

                string listText;
                if (useDate)
                {
                    listText = alarm.Date.ToString("yyyy.MM.dd\tH:mm");
                }
                else
                {
                    listText = alarm.Date.ToString("H:mm");
                }

                listBoxAlarmClock.Items.Add(listText);
                //string selectTime = addAlarmDialog.dateTime.ToString("H:mm");
                //listBoxAlarmClock.Items.Add(selectTime);
            }
        }

        private void listBoxAlarmClock_DoubleClick(object sender, EventArgs e)
        {
            var selectedItem = listBoxAlarmClock.SelectedItem;
            if (selectedItem != null)
            {
                var result = MessageBox.Show($"Remove? {selectedItem}", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Удаляем выбранный элемент
                    listBoxAlarmClock.Items.Remove(selectedItem);
                }
            }
        }

        
    }
}

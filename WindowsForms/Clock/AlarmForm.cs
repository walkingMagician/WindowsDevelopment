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
        public ListBox Alarms { get => listBoxAlarmClock; }
        public AlarmForm()
        {
            InitializeComponent();

            if (addAlarmDialog == null) addAlarmDialog = new AddAlarmDialog();

          
        }


        public AlarmForm(System.Windows.Forms.Form parent) : this()
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point
                (
                parent.Location.X-this.Width,
                parent.Location.Y
                );
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            addAlarmDialog.Location = new Point
                (
                this.Location.X + (this.Width - addAlarmDialog.Width) / 2,
                this.Location.Y + (this.Height - addAlarmDialog.Height) / 2
                );
            DialogResult result = addAlarmDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                listBoxAlarmClock.Items.Add(new Alarm(addAlarmDialog.Alarm));
            }
            
            
            /*if (addAlarmDialog.ShowDialog() == DialogResult.OK)
            {
                alarm.Date = addAlarmDialog.dateTime;
                bool useDate = addAlarmDialog.useDate;

                if (useDate)
                {
                    //listText = alarm.Date.ToString("yyyy.MM.dd\tH:mm");
                    listBoxAlarmClock.Items.Add(alarm.Date.ToString("yyyy.MM.dd\tH:mm"));
                    
                }
                else
                {
                    listBoxAlarmClock.Items.Add(alarm.Date.ToString("H:mm"));
                }

                //listBoxAlarmClock.Items.Add(listText);
                //string selectTime = addAlarmDialog.dateTime.ToString("H:mm");
                //listBoxAlarmClock.Items.Add(selectTime);
            }*/
        }

        private void listBoxAlarmClock_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxAlarmClock.SelectedItem == null) return;
            addAlarmDialog.Alarm = listBoxAlarmClock.SelectedItem as Alarm;
            if (addAlarmDialog.ShowDialog() == DialogResult.OK)
            {
                listBoxAlarmClock.Items[listBoxAlarmClock.SelectedIndex] = addAlarmDialog.Alarm;

            }


            /*var selectedItem = listBoxAlarmClock.SelectedItem;
            if (selectedItem != null)
            {
                DialogResult result = MessageBox.Show($"Изменить - Удалить - Отмена? {selectedItem}", "Изменение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    addAlarmDialog.ShowDialog();
                    listBoxAlarmClock.Items.Remove(selectedItem);
                    alarm.Date = addAlarmDialog.dateTime;
                    bool useDate = addAlarmDialog.useDate;

                    if (useDate)
                    {
                        //listText = alarm.Date.ToString("yyyy.MM.dd\tH:mm");
                        listBoxAlarmClock.Items.Add(alarm.Date.ToString("yyyy.MM.dd\tH:mm"));

                    }
                    else
                    {
                        listBoxAlarmClock.Items.Add(alarm.Date.ToString("H:mm"));
                    }
                }
                if(result == DialogResult.No) 
                {
                    // Удаляем выбранный элемент
                    listBoxAlarmClock.Items.Remove(selectedItem);
                }
            }*/
        }

        private void listBoxAlarmClock_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBoxAlarmClock.SelectedItems != null)
                labelAlarmInfo.Text = listBoxAlarmClock.SelectedItems.ToString();
        }


        /* private void ButtonDelete_Click(object sender, EventArgs e)
         {
             var selectedItem = listBoxAlarmClock.SelectedItem;
             if (selectedItem != null)
             {
                 DialogResult result = MessageBox.Show($"Remove? {selectedItem}", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (result == DialogResult.Yes)
                 {
                     // Удаляем выбранный элемент
                     listBoxAlarmClock.Items.Remove(selectedItem);
                 }
             }
         }*/

    }
}

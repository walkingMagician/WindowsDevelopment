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
        private Timer timer;
        public AlarmForm()
        {
            InitializeComponent();

            if (addAlarmDialog == null) addAlarmDialog = new AddAlarmDialog();

            // Инициализация таймера
            /*timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();*/
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
            var selectedItem = listBoxAlarmClock.SelectedItem;
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
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (DateTime dateTime in listBoxAlarmClock.Items)
            {
                if (dateTime <= DateTime.Now)
                {
                    // Звенит, если текущее время совпадает или превышает установленное
                    System.Media.SystemSounds.Beep.Play();
                    MessageBox.Show("The alarm  went off", "Alarm clock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listBoxAlarmClock.Items.Remove(dateTime);

                    // Удалить событие, чтобы не звенеть повторно
                    listBoxAlarmClock.Items.Remove(dateTime);
                    break; // Остановить цикл после первой найденной даты
                }
            }
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

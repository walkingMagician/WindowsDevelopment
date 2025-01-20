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
    public partial class AlarmClock : Form
    {
        private List<TimeSpan> timeSpans = new List<TimeSpan>();
        private Timer Timer;

        public AlarmClock()
        {
            InitializeComponent();
            System.Media.SystemSounds.Beep.Play();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            timer.Start();
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            foreach (TimeSpan ts in timeSpans)
            {
                if (now.TimeOfDay >= ts && now.TimeOfDay < ts.Add(new TimeSpan(0, 0, 1)))
                {
                    System.Media.SystemSounds.Beep.Play();
                    MessageBox.Show("The alarm  went off", "Alarm clock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    timeSpans.Remove(ts);
                    UpdateAlarmList();
                    break;
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string inputTime = textBoxAlarmTime.Text.Trim();
                /*DateTime dateTime = DateTime.ParseExact(inputTime, "H:mm",
                    CultureInfo.InvariantCulture);
                dateTime = dateTime.Date.Add(dateTime.TimeOfDay);*/
                TimeSpan timeSpan = TimeSpan.Parse(inputTime);
                //DateTime dateTime = dateTimePicker.Value.Date.Add(timeSpan);


                /*if (dateTime < DateTime.Now)
                {
                    MessageBox.Show("The alarm cannot be set in the past", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }*/
                if (!timeSpans.Contains(timeSpan))
                {
                    timeSpans.Add(timeSpan);
                    UpdateAlarmList();
                    textBoxAlarmTime.Clear();
                }
                else
                {
                    MessageBox.Show("Error this alarm has already been set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Некоректный формат времени, пожалуйста используйте формат ЧЧ:ММ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonToChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxAlarmClock.SelectedItems != null)
                {
                    //DateTime selectedAlarm = (DateTime)listBoxAlarmClock.SelectedItem;
                    //DateTime.Remove(selectedAlarm);
                    string selectedAlarm = listBoxAlarmClock.SelectedItem.ToString();
                    TimeSpan alarmToChange = TimeSpan.Parse(selectedAlarm);
                    string inputTime = textBoxAlarmTime.Text.Trim();
                    TimeSpan timeSpan = TimeSpan.Parse(inputTime);
                    timeSpans.Remove(alarmToChange);
                    timeSpans.Add(timeSpan);
                    textBoxAlarmTime.Clear();
                    UpdateAlarmList();

                    //DateTime newAlarmTime = dateTimePicker.Value;
                    /*if (TimeSpan.Parse(textBoxAlarmTime.Text.Trim(), "H:mm",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out newAlarmTime))
                    {
                        newAlarmTime = newAlarmTime.Date.Add(newAlarmTime.TimeOfDay);
                        //timeSpans.Add(newAlarmTime);
                        textBoxAlarmTime.Clear();
                    }*/
                   //else
                    //{
                        //MessageBox.Show("Некорректный формат времени. Пожалуйста, используйте формат ЧЧ:ММ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не выбран элимент для изменения", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxAlarmClock.SelectedItems != null)
                {
                    string selectedAlarm = listBoxAlarmClock.SelectedItem.ToString();
                    TimeSpan alarmRemuve = TimeSpan.Parse(selectedAlarm);
                    
                    if(timeSpans.Remove(alarmRemuve))
                        UpdateAlarmList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не выбран элимент для удаления", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateAlarmList() // обновление листа
        {
            listBoxAlarmClock.Items.Clear();
            foreach (TimeSpan dateTime in timeSpans)
            {
                //listBoxAlarmClock.Items.Add(dateTime);
                listBoxAlarmClock.Items.Add(dateTime.ToString());
            }
        }

        
    }
}

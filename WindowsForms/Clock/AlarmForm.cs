using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
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
        Alarm alarm;
        public AlarmForm()
        {
            InitializeComponent();
            this.FormClosing += AlarmForm_FormClosing;

            if (addAlarmDialog == null) addAlarmDialog = new AddAlarmDialog();

            LoadSettingsData();
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

        /*public void AddDataTime(string[] dataTime)
        {
            listBoxAlarmClock.Items.Add(dataTime);
        }*/

        public void SaveSettingsData()
        {
            string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\..\\..\\SettingData.ini";
            try
            {
                List<string> items = new List<string>();
                foreach (var item in listBoxAlarmClock.Items)
                { 
                    items.Add(item.ToString());
                }
                File.WriteAllLines(path, items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "In SaveSettingsData()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(this, ex.ToString(), "In SaveSettingsData()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        public void LoadSettingsData()
        {
            string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\..\\..\\SettingData.ini";
            try
            {
                
                if (File.Exists(path))
                {
                    
                    string[] lines = File.ReadAllLines(path);
                    //listBoxAlarmClock.Items.AddRange(lines);
                    //alarm = lines.Cast<Alarm>().ToArray().Min();
                    //listBoxAlarmClock.Items.Add(alarm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "In LoadSettingsData()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(this, ex.ToString(), "In LoadSettingsData()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void AlarmForm_FormClosing(object sender, FormClosingEventArgs e)
        { 
            SaveSettingsData();
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

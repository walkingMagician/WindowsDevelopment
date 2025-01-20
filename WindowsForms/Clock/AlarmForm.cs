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
        
        public AlarmForm()
        {
            InitializeComponent();
            if(addAlarmDialog == null ) addAlarmDialog = new AddAlarmDialog();
        }
        

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            addAlarmDialog = new AddAlarmDialog();
            if (addAlarmDialog.ShowDialog() == DialogResult.OK)
            {
                string selectTime = addAlarmDialog.DateTime.ToString("H:mm");
                listBoxAlarmClock.Items.Add(selectTime);
            }
        }
    }
}

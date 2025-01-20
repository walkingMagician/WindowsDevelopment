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

        //public List<DateTime> listTime = new List<DateTime>();
        public DateTime DateTime { get; set; }
        public AddAlarmDialog()
        {
            InitializeComponent();
            
        }

        private void checkBoxUseDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerDate.Enabled = checkBoxUseDate.Checked;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DateTime = dateTimePickerTime.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        
    }
}

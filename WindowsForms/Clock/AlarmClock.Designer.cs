namespace Clock
{
    partial class AlarmClock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.listBoxAlarmClock = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonToChange = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.textBoxAlarmTime = new System.Windows.Forms.TextBox();
            this.labelTimeFormat = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(16, 39);
            this.dateTimePicker.Margin = new System.Windows.Forms.Padding(6);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(209, 29);
            this.dateTimePicker.TabIndex = 0;
            // 
            // listBoxAlarmClock
            // 
            this.listBoxAlarmClock.FormattingEnabled = true;
            this.listBoxAlarmClock.ItemHeight = 24;
            this.listBoxAlarmClock.Location = new System.Drawing.Point(16, 136);
            this.listBoxAlarmClock.Name = "listBoxAlarmClock";
            this.listBoxAlarmClock.Size = new System.Drawing.Size(209, 124);
            this.listBoxAlarmClock.TabIndex = 1;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.Location = new System.Drawing.Point(231, 136);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(89, 28);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonToChange
            // 
            this.buttonToChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonToChange.Location = new System.Drawing.Point(231, 170);
            this.buttonToChange.Name = "buttonToChange";
            this.buttonToChange.Size = new System.Drawing.Size(89, 28);
            this.buttonToChange.TabIndex = 3;
            this.buttonToChange.Text = "To Change";
            this.buttonToChange.UseVisualStyleBackColor = true;
            this.buttonToChange.Click += new System.EventHandler(this.buttonToChange_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDelete.Location = new System.Drawing.Point(231, 204);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(89, 28);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(322, 297);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // textBoxAlarmTime
            // 
            this.textBoxAlarmTime.Location = new System.Drawing.Point(16, 101);
            this.textBoxAlarmTime.MaxLength = 8;
            this.textBoxAlarmTime.Name = "textBoxAlarmTime";
            this.textBoxAlarmTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxAlarmTime.Size = new System.Drawing.Size(104, 29);
            this.textBoxAlarmTime.TabIndex = 6;
            this.textBoxAlarmTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelTimeFormat
            // 
            this.labelTimeFormat.AutoSize = true;
            this.labelTimeFormat.Location = new System.Drawing.Point(12, 74);
            this.labelTimeFormat.Name = "labelTimeFormat";
            this.labelTimeFormat.Size = new System.Drawing.Size(314, 24);
            this.labelTimeFormat.TabIndex = 7;
            this.labelTimeFormat.Text = "Введите время в формате ЧЧ:ММ";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(12, 9);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(351, 24);
            this.labelDate.TabIndex = 8;
            this.labelDate.Text = "Выберите дату (формат) ДД ММ ГГГГ";
            // 
            // AlarmClock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 332);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelTimeFormat);
            this.Controls.Add(this.textBoxAlarmTime);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonToChange);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.listBoxAlarmClock);
            this.Controls.Add(this.dateTimePicker);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "AlarmClock";
            this.Text = "Alarm clock";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.ListBox listBoxAlarmClock;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonToChange;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox textBoxAlarmTime;
        private System.Windows.Forms.Label labelTimeFormat;
        private System.Windows.Forms.Label labelDate;
    }
}
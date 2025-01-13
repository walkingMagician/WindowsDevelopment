namespace Clock
{
    partial class FontDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontDialog));
            this.comboBoxFonts = new System.Windows.Forms.ComboBox();
            this.labelChooseFont = new System.Windows.Forms.Label();
            this.labelSizeFont = new System.Windows.Forms.Label();
            this.numericUpDownFontSize = new System.Windows.Forms.NumericUpDown();
            this.labelExample = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxFonts
            // 
            this.comboBoxFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFonts.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxFonts.FormattingEnabled = true;
            this.comboBoxFonts.Location = new System.Drawing.Point(12, 24);
            this.comboBoxFonts.Name = "comboBoxFonts";
            this.comboBoxFonts.Size = new System.Drawing.Size(317, 32);
            this.comboBoxFonts.TabIndex = 0;
            // 
            // labelChooseFont
            // 
            this.labelChooseFont.AutoSize = true;
            this.labelChooseFont.Location = new System.Drawing.Point(12, 8);
            this.labelChooseFont.Name = "labelChooseFont";
            this.labelChooseFont.Size = new System.Drawing.Size(92, 13);
            this.labelChooseFont.TabIndex = 1;
            this.labelChooseFont.Text = "выберите шрифт";
            // 
            // labelSizeFont
            // 
            this.labelSizeFont.AutoSize = true;
            this.labelSizeFont.Location = new System.Drawing.Point(335, 8);
            this.labelSizeFont.Name = "labelSizeFont";
            this.labelSizeFont.Size = new System.Drawing.Size(88, 13);
            this.labelSizeFont.TabIndex = 2;
            this.labelSizeFont.Text = "Размер шрифтв";
            // 
            // numericUpDownFontSize
            // 
            this.numericUpDownFontSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDownFontSize.Location = new System.Drawing.Point(338, 24);
            this.numericUpDownFontSize.Maximum = new decimal(new int[] {
            58,
            0,
            0,
            0});
            this.numericUpDownFontSize.Minimum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.numericUpDownFontSize.Name = "numericUpDownFontSize";
            this.numericUpDownFontSize.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numericUpDownFontSize.Size = new System.Drawing.Size(120, 29);
            this.numericUpDownFontSize.TabIndex = 3;
            this.numericUpDownFontSize.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericUpDownFontSize.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // labelExample
            // 
            this.labelExample.AutoSize = true;
            this.labelExample.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelExample.Location = new System.Drawing.Point(12, 154);
            this.labelExample.Name = "labelExample";
            this.labelExample.Size = new System.Drawing.Size(163, 42);
            this.labelExample.TabIndex = 4;
            this.labelExample.Text = "Example";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(381, 258);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(300, 258);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(338, 60);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(118, 23);
            this.buttonApply.TabIndex = 7;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // FontDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 293);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelExample);
            this.Controls.Add(this.numericUpDownFontSize);
            this.Controls.Add(this.labelSizeFont);
            this.Controls.Add(this.labelChooseFont);
            this.Controls.Add(this.comboBoxFonts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FontDialog";
            this.Text = "FontDialog";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxFonts;
        private System.Windows.Forms.Label labelChooseFont;
        private System.Windows.Forms.Label labelSizeFont;
        private System.Windows.Forms.NumericUpDown numericUpDownFontSize;
        private System.Windows.Forms.Label labelExample;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonApply;
    }
}
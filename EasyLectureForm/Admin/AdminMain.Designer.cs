namespace EasyLectureForm.Admin
{
    partial class AdminMain
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
            this.NameLbl = new System.Windows.Forms.Label();
            this.MailLbl = new System.Windows.Forms.Label();
            this.PassLbl = new System.Windows.Forms.Label();
            this.NameTxb = new System.Windows.Forms.TextBox();
            this.MailTxb = new System.Windows.Forms.TextBox();
            this.PassTxb = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.TypeTxb = new System.Windows.Forms.Label();
            this.LectureLbx = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // NameLbl
            // 
            this.NameLbl.AutoSize = true;
            this.NameLbl.Location = new System.Drawing.Point(48, 46);
            this.NameLbl.Name = "NameLbl";
            this.NameLbl.Size = new System.Drawing.Size(35, 13);
            this.NameLbl.TabIndex = 0;
            this.NameLbl.Text = "Name";
            // 
            // MailLbl
            // 
            this.MailLbl.AutoSize = true;
            this.MailLbl.Location = new System.Drawing.Point(57, 88);
            this.MailLbl.Name = "MailLbl";
            this.MailLbl.Size = new System.Drawing.Size(26, 13);
            this.MailLbl.TabIndex = 1;
            this.MailLbl.Text = "Mail";
            // 
            // PassLbl
            // 
            this.PassLbl.AutoSize = true;
            this.PassLbl.Location = new System.Drawing.Point(30, 134);
            this.PassLbl.Name = "PassLbl";
            this.PassLbl.Size = new System.Drawing.Size(53, 13);
            this.PassLbl.TabIndex = 2;
            this.PassLbl.Text = "Password";
            // 
            // NameTxb
            // 
            this.NameTxb.Location = new System.Drawing.Point(105, 43);
            this.NameTxb.Name = "NameTxb";
            this.NameTxb.Size = new System.Drawing.Size(188, 20);
            this.NameTxb.TabIndex = 3;
            // 
            // MailTxb
            // 
            this.MailTxb.Location = new System.Drawing.Point(105, 85);
            this.MailTxb.Name = "MailTxb";
            this.MailTxb.Size = new System.Drawing.Size(188, 20);
            this.MailTxb.TabIndex = 4;
            // 
            // PassTxb
            // 
            this.PassTxb.Location = new System.Drawing.Point(105, 134);
            this.PassTxb.Name = "PassTxb";
            this.PassTxb.Size = new System.Drawing.Size(188, 20);
            this.PassTxb.TabIndex = 5;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(105, 184);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(188, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // TypeTxb
            // 
            this.TypeTxb.AutoSize = true;
            this.TypeTxb.Location = new System.Drawing.Point(30, 187);
            this.TypeTxb.Name = "TypeTxb";
            this.TypeTxb.Size = new System.Drawing.Size(31, 13);
            this.TypeTxb.TabIndex = 7;
            this.TypeTxb.Text = "Type";
            // 
            // LectureLbx
            // 
            this.LectureLbx.FormattingEnabled = true;
            this.LectureLbx.Location = new System.Drawing.Point(33, 286);
            this.LectureLbx.Name = "LectureLbx";
            this.LectureLbx.Size = new System.Drawing.Size(402, 94);
            this.LectureLbx.TabIndex = 8;
            // 
            // AdminMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 446);
            this.Controls.Add(this.LectureLbx);
            this.Controls.Add(this.TypeTxb);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.PassTxb);
            this.Controls.Add(this.MailTxb);
            this.Controls.Add(this.NameTxb);
            this.Controls.Add(this.PassLbl);
            this.Controls.Add(this.MailLbl);
            this.Controls.Add(this.NameLbl);
            this.Name = "AdminMain";
            this.Text = "AdminMain";
            this.Load += new System.EventHandler(this.AdminMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.Label MailLbl;
        private System.Windows.Forms.Label PassLbl;
        private System.Windows.Forms.TextBox NameTxb;
        private System.Windows.Forms.TextBox MailTxb;
        private System.Windows.Forms.TextBox PassTxb;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label TypeTxb;
        private System.Windows.Forms.CheckedListBox LectureLbx;
    }
}
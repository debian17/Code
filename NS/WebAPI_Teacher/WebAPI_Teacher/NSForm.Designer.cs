namespace WebAPI_Teacher
{
    partial class NSForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NSForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoginTB = new System.Windows.Forms.TextBox();
            this.PasswordTB = new System.Windows.Forms.TextBox();
            this.SignInBTN = new System.Windows.Forms.Button();
            this.DepartmentChB = new System.Windows.Forms.CheckedListBox();
            this.GroupChB = new System.Windows.Forms.CheckedListBox();
            this.AllDepChB = new System.Windows.Forms.CheckBox();
            this.SendFacultyRB = new System.Windows.Forms.RadioButton();
            this.SendDepartmentRB = new System.Windows.Forms.RadioButton();
            this.SendGroupRB = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.MessageRTB = new System.Windows.Forms.RichTextBox();
            this.SendBTN = new System.Windows.Forms.Button();
            this.DepartmentCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ThemeTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(396, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Логин:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(396, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Пароль:";
            // 
            // LoginTB
            // 
            this.LoginTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoginTB.Location = new System.Drawing.Point(400, 200);
            this.LoginTB.Name = "LoginTB";
            this.LoginTB.Size = new System.Drawing.Size(250, 26);
            this.LoginTB.TabIndex = 2;
            // 
            // PasswordTB
            // 
            this.PasswordTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PasswordTB.Location = new System.Drawing.Point(400, 280);
            this.PasswordTB.Name = "PasswordTB";
            this.PasswordTB.PasswordChar = '*';
            this.PasswordTB.Size = new System.Drawing.Size(250, 26);
            this.PasswordTB.TabIndex = 3;
            // 
            // SignInBTN
            // 
            this.SignInBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SignInBTN.Location = new System.Drawing.Point(400, 350);
            this.SignInBTN.Name = "SignInBTN";
            this.SignInBTN.Size = new System.Drawing.Size(250, 60);
            this.SignInBTN.TabIndex = 4;
            this.SignInBTN.Text = "Вход";
            this.SignInBTN.UseVisualStyleBackColor = true;
            this.SignInBTN.Click += new System.EventHandler(this.SignInBTN_Click);
            // 
            // DepartmentChB
            // 
            this.DepartmentChB.CheckOnClick = true;
            this.DepartmentChB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DepartmentChB.FormattingEnabled = true;
            this.DepartmentChB.Location = new System.Drawing.Point(31, 193);
            this.DepartmentChB.Name = "DepartmentChB";
            this.DepartmentChB.Size = new System.Drawing.Size(200, 298);
            this.DepartmentChB.TabIndex = 5;
            this.DepartmentChB.SelectedIndexChanged += new System.EventHandler(this.DepartmentChB_SelectedIndexChanged);
            // 
            // GroupChB
            // 
            this.GroupChB.CheckOnClick = true;
            this.GroupChB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GroupChB.FormattingEnabled = true;
            this.GroupChB.Location = new System.Drawing.Point(266, 193);
            this.GroupChB.Name = "GroupChB";
            this.GroupChB.Size = new System.Drawing.Size(200, 298);
            this.GroupChB.TabIndex = 6;
            // 
            // AllDepChB
            // 
            this.AllDepChB.AutoSize = true;
            this.AllDepChB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AllDepChB.Location = new System.Drawing.Point(34, 163);
            this.AllDepChB.Name = "AllDepChB";
            this.AllDepChB.Size = new System.Drawing.Size(136, 24);
            this.AllDepChB.TabIndex = 7;
            this.AllDepChB.Text = "Выбрать все";
            this.AllDepChB.UseVisualStyleBackColor = true;
            this.AllDepChB.CheckedChanged += new System.EventHandler(this.AllDepChB_CheckedChanged);
            // 
            // SendFacultyRB
            // 
            this.SendFacultyRB.AutoSize = true;
            this.SendFacultyRB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SendFacultyRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SendFacultyRB.Location = new System.Drawing.Point(36, 24);
            this.SendFacultyRB.Name = "SendFacultyRB";
            this.SendFacultyRB.Size = new System.Drawing.Size(287, 25);
            this.SendFacultyRB.TabIndex = 8;
            this.SendFacultyRB.TabStop = true;
            this.SendFacultyRB.Text = "Отправить всему факультету";
            this.SendFacultyRB.UseVisualStyleBackColor = true;
            this.SendFacultyRB.CheckedChanged += new System.EventHandler(this.SendFacultyRB_CheckedChanged);
            // 
            // SendDepartmentRB
            // 
            this.SendDepartmentRB.AutoSize = true;
            this.SendDepartmentRB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SendDepartmentRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SendDepartmentRB.Location = new System.Drawing.Point(36, 70);
            this.SendDepartmentRB.Name = "SendDepartmentRB";
            this.SendDepartmentRB.Size = new System.Drawing.Size(252, 25);
            this.SendDepartmentRB.TabIndex = 9;
            this.SendDepartmentRB.TabStop = true;
            this.SendDepartmentRB.Text = "Отправить всей кафедре";
            this.SendDepartmentRB.UseVisualStyleBackColor = true;
            this.SendDepartmentRB.CheckedChanged += new System.EventHandler(this.SendDepartmentRB_CheckedChanged);
            // 
            // SendGroupRB
            // 
            this.SendGroupRB.AutoSize = true;
            this.SendGroupRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SendGroupRB.Location = new System.Drawing.Point(36, 117);
            this.SendGroupRB.Name = "SendGroupRB";
            this.SendGroupRB.Size = new System.Drawing.Size(234, 24);
            this.SendGroupRB.TabIndex = 10;
            this.SendGroupRB.TabStop = true;
            this.SendGroupRB.Text = "Отправить набору групп";
            this.SendGroupRB.UseVisualStyleBackColor = true;
            this.SendGroupRB.CheckedChanged += new System.EventHandler(this.SendGroupRB_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(585, 249);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Текст сообщения:";
            // 
            // MessageRTB
            // 
            this.MessageRTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MessageRTB.Location = new System.Drawing.Point(584, 272);
            this.MessageRTB.Name = "MessageRTB";
            this.MessageRTB.Size = new System.Drawing.Size(397, 219);
            this.MessageRTB.TabIndex = 12;
            this.MessageRTB.Text = "";
            // 
            // SendBTN
            // 
            this.SendBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SendBTN.Location = new System.Drawing.Point(400, 542);
            this.SendBTN.Name = "SendBTN";
            this.SendBTN.Size = new System.Drawing.Size(250, 60);
            this.SendBTN.TabIndex = 13;
            this.SendBTN.Text = "Отправить";
            this.SendBTN.UseVisualStyleBackColor = true;
            this.SendBTN.Click += new System.EventHandler(this.SendBTN_Click);
            // 
            // DepartmentCB
            // 
            this.DepartmentCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DepartmentCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DepartmentCB.FormattingEnabled = true;
            this.DepartmentCB.Location = new System.Drawing.Point(282, 73);
            this.DepartmentCB.Name = "DepartmentCB";
            this.DepartmentCB.Size = new System.Drawing.Size(121, 28);
            this.DepartmentCB.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(580, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Тема:";
            // 
            // ThemeTB
            // 
            this.ThemeTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ThemeTB.Location = new System.Drawing.Point(584, 194);
            this.ThemeTB.Name = "ThemeTB";
            this.ThemeTB.Size = new System.Drawing.Size(397, 26);
            this.ThemeTB.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(400, 427);
            this.label5.MaximumSize = new System.Drawing.Size(250, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 20);
            this.label5.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(695, 562);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 20);
            this.label6.TabIndex = 18;
            // 
            // NSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 684);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ThemeTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DepartmentCB);
            this.Controls.Add(this.SendBTN);
            this.Controls.Add(this.MessageRTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SendGroupRB);
            this.Controls.Add(this.SendDepartmentRB);
            this.Controls.Add(this.SendFacultyRB);
            this.Controls.Add(this.AllDepChB);
            this.Controls.Add(this.GroupChB);
            this.Controls.Add(this.DepartmentChB);
            this.Controls.Add(this.PasswordTB);
            this.Controls.Add(this.LoginTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SignInBTN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NSForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NSForm";
            this.Load += new System.EventHandler(this.NSForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LoginTB;
        private System.Windows.Forms.TextBox PasswordTB;
        private System.Windows.Forms.Button SignInBTN;
        private System.Windows.Forms.CheckedListBox DepartmentChB;
        private System.Windows.Forms.CheckedListBox GroupChB;
        private System.Windows.Forms.CheckBox AllDepChB;
        private System.Windows.Forms.RadioButton SendFacultyRB;
        private System.Windows.Forms.RadioButton SendDepartmentRB;
        private System.Windows.Forms.RadioButton SendGroupRB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox MessageRTB;
        private System.Windows.Forms.Button SendBTN;
        private System.Windows.Forms.ComboBox DepartmentCB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ThemeTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
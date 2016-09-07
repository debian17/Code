using System.ComponentModel.DataAnnotations;

namespace WebAPI_Client
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BookNumberTB = new System.Windows.Forms.TextBox();
            this.PhoneNumberTB = new System.Windows.Forms.TextBox();
            this.EmailAddressTB = new System.Windows.Forms.TextBox();
            this.RegBTN = new System.Windows.Forms.Button();
            this.RegCB = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.PasswordTB = new System.Windows.Forms.TextBox();
            this.CPasswordTB = new System.Windows.Forms.TextBox();
            this.FacultyCB = new System.Windows.Forms.ComboBox();
            this.DepartmentCB = new System.Windows.Forms.ComboBox();
            this.GroupCB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(70, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер зачетки:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(408, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Кафедра:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(408, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Факультет:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(403, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Учебная группа:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(70, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(161, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "Номер телефона:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(70, 316);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Email адрес:";
            // 
            // BookNumberTB
            // 
            this.BookNumberTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BookNumberTB.Location = new System.Drawing.Point(74, 62);
            this.BookNumberTB.Name = "BookNumberTB";
            this.BookNumberTB.Size = new System.Drawing.Size(253, 26);
            this.BookNumberTB.TabIndex = 10;
            this.BookNumberTB.TextChanged += new System.EventHandler(this.BookNumberTB_TextChanged);
            // 
            // PhoneNumberTB
            // 
            this.PhoneNumberTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PhoneNumberTB.Location = new System.Drawing.Point(74, 269);
            this.PhoneNumberTB.Name = "PhoneNumberTB";
            this.PhoneNumberTB.Size = new System.Drawing.Size(253, 26);
            this.PhoneNumberTB.TabIndex = 11;
            this.PhoneNumberTB.TextChanged += new System.EventHandler(this.PhoneNumberTB_TextChanged);
            // 
            // EmailAddressTB
            // 
            this.EmailAddressTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EmailAddressTB.Location = new System.Drawing.Point(74, 339);
            this.EmailAddressTB.Name = "EmailAddressTB";
            this.EmailAddressTB.Size = new System.Drawing.Size(253, 26);
            this.EmailAddressTB.TabIndex = 12;
            this.EmailAddressTB.TextChanged += new System.EventHandler(this.EmailAddressTB_TextChanged);
            // 
            // RegBTN
            // 
            this.RegBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RegBTN.Location = new System.Drawing.Point(406, 313);
            this.RegBTN.Name = "RegBTN";
            this.RegBTN.Size = new System.Drawing.Size(254, 57);
            this.RegBTN.TabIndex = 20;
            this.RegBTN.Text = "Регистрация";
            this.RegBTN.UseVisualStyleBackColor = true;
            this.RegBTN.Click += new System.EventHandler(this.RegBTN_Click);
            // 
            // RegCB
            // 
            this.RegCB.AutoSize = true;
            this.RegCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RegCB.Location = new System.Drawing.Point(406, 275);
            this.RegCB.Name = "RegCB";
            this.RegCB.Size = new System.Drawing.Size(15, 14);
            this.RegCB.TabIndex = 21;
            this.RegCB.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(427, 258);
            this.label11.MaximumSize = new System.Drawing.Size(250, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(238, 48);
            this.label11.TabIndex = 22;
            this.label11.Text = "Я подтверждаю правильность введенных мною данных и даю согласие на их обработку.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(70, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 20);
            this.label7.TabIndex = 23;
            this.label7.Text = "Пароль:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(70, 181);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(197, 20);
            this.label8.TabIndex = 24;
            this.label8.Text = "Подтвердите пароль:";
            // 
            // PasswordTB
            // 
            this.PasswordTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PasswordTB.Location = new System.Drawing.Point(74, 131);
            this.PasswordTB.Name = "PasswordTB";
            this.PasswordTB.PasswordChar = '*';
            this.PasswordTB.Size = new System.Drawing.Size(253, 26);
            this.PasswordTB.TabIndex = 25;
            this.PasswordTB.TextChanged += new System.EventHandler(this.PasswordTB_TextChanged);
            // 
            // CPasswordTB
            // 
            this.CPasswordTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CPasswordTB.Location = new System.Drawing.Point(74, 204);
            this.CPasswordTB.Name = "CPasswordTB";
            this.CPasswordTB.PasswordChar = '*';
            this.CPasswordTB.Size = new System.Drawing.Size(253, 26);
            this.CPasswordTB.TabIndex = 26;
            this.CPasswordTB.TextChanged += new System.EventHandler(this.CPasswordTB_TextChanged);
            // 
            // FacultyCB
            // 
            this.FacultyCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FacultyCB.FormattingEnabled = true;
            this.FacultyCB.Location = new System.Drawing.Point(412, 60);
            this.FacultyCB.Name = "FacultyCB";
            this.FacultyCB.Size = new System.Drawing.Size(248, 28);
            this.FacultyCB.TabIndex = 27;
            this.FacultyCB.SelectedIndexChanged += new System.EventHandler(this.FacultyCB_SelectedIndexChanged);
            // 
            // DepartmentCB
            // 
            this.DepartmentCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DepartmentCB.FormattingEnabled = true;
            this.DepartmentCB.Location = new System.Drawing.Point(412, 133);
            this.DepartmentCB.Name = "DepartmentCB";
            this.DepartmentCB.Size = new System.Drawing.Size(248, 28);
            this.DepartmentCB.TabIndex = 28;
            this.DepartmentCB.SelectedIndexChanged += new System.EventHandler(this.DepartmentCB_SelectedIndexChanged);
            // 
            // GroupCB
            // 
            this.GroupCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GroupCB.FormattingEnabled = true;
            this.GroupCB.Location = new System.Drawing.Point(407, 203);
            this.GroupCB.Name = "GroupCB";
            this.GroupCB.Size = new System.Drawing.Size(253, 28);
            this.GroupCB.TabIndex = 29;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 407);
            this.Controls.Add(this.GroupCB);
            this.Controls.Add(this.DepartmentCB);
            this.Controls.Add(this.FacultyCB);
            this.Controls.Add(this.CPasswordTB);
            this.Controls.Add(this.PasswordTB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.RegCB);
            this.Controls.Add(this.RegBTN);
            this.Controls.Add(this.EmailAddressTB);
            this.Controls.Add(this.PhoneNumberTB);
            this.Controls.Add(this.BookNumberTB);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Регистрация студента";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox BookNumberTB;
        private System.Windows.Forms.TextBox PhoneNumberTB;
        private System.Windows.Forms.TextBox EmailAddressTB;
        private System.Windows.Forms.Button RegBTN;
        private System.Windows.Forms.CheckBox RegCB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox PasswordTB;
        private System.Windows.Forms.TextBox CPasswordTB;
        private System.Windows.Forms.ComboBox FacultyCB;
        private System.Windows.Forms.ComboBox DepartmentCB;
        private System.Windows.Forms.ComboBox GroupCB;
    }
}


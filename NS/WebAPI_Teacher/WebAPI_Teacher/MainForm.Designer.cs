namespace WebAPI_Teacher
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
            this.SignInBTN = new System.Windows.Forms.Button();
            this.RegBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SignInBTN
            // 
            this.SignInBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SignInBTN.Location = new System.Drawing.Point(47, 22);
            this.SignInBTN.Name = "SignInBTN";
            this.SignInBTN.Size = new System.Drawing.Size(300, 100);
            this.SignInBTN.TabIndex = 0;
            this.SignInBTN.Text = "Вход в систему";
            this.SignInBTN.UseVisualStyleBackColor = true;
            this.SignInBTN.Click += new System.EventHandler(this.SignInBTN_Click);
            // 
            // RegBTN
            // 
            this.RegBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RegBTN.Location = new System.Drawing.Point(47, 173);
            this.RegBTN.Name = "RegBTN";
            this.RegBTN.Size = new System.Drawing.Size(300, 100);
            this.RegBTN.TabIndex = 1;
            this.RegBTN.Text = "Регистрация в системе";
            this.RegBTN.UseVisualStyleBackColor = true;
            this.RegBTN.Click += new System.EventHandler(this.RegBTN_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 317);
            this.Controls.Add(this.RegBTN);
            this.Controls.Add(this.SignInBTN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рассылка сообщений";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SignInBTN;
        private System.Windows.Forms.Button RegBTN;
    }
}


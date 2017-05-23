namespace Bike18InVK
{
    partial class Form1
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
            this.tbNethouseLogin = new System.Windows.Forms.TextBox();
            this.tbNethousePass = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbVkLogin = new System.Windows.Forms.TextBox();
            this.tbVkPass = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNethouseLogin
            // 
            this.tbNethouseLogin.Location = new System.Drawing.Point(6, 19);
            this.tbNethouseLogin.Name = "tbNethouseLogin";
            this.tbNethouseLogin.Size = new System.Drawing.Size(100, 20);
            this.tbNethouseLogin.TabIndex = 0;
            // 
            // tbNethousePass
            // 
            this.tbNethousePass.Location = new System.Drawing.Point(112, 19);
            this.tbNethousePass.Name = "tbNethousePass";
            this.tbNethousePass.PasswordChar = '+';
            this.tbNethousePass.Size = new System.Drawing.Size(100, 20);
            this.tbNethousePass.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNethouseLogin);
            this.groupBox1.Controls.Add(this.tbNethousePass);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 48);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "nethouse";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbVkLogin);
            this.groupBox2.Controls.Add(this.tbVkPass);
            this.groupBox2.Location = new System.Drawing.Point(12, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(219, 48);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "vk";
            // 
            // tbVkLogin
            // 
            this.tbVkLogin.Location = new System.Drawing.Point(6, 19);
            this.tbVkLogin.Name = "tbVkLogin";
            this.tbVkLogin.Size = new System.Drawing.Size(100, 20);
            this.tbVkLogin.TabIndex = 0;
            // 
            // tbVkPass
            // 
            this.tbVkPass.Location = new System.Drawing.Point(112, 19);
            this.tbVkPass.Name = "tbVkPass";
            this.tbVkPass.PasswordChar = '+';
            this.tbVkPass.Size = new System.Drawing.Size(100, 20);
            this.tbVkPass.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 120);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(219, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Начать перенос";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 261);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Перенос на ВК";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbNethouseLogin;
        private System.Windows.Forms.TextBox tbNethousePass;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbVkLogin;
        private System.Windows.Forms.TextBox tbVkPass;
        private System.Windows.Forms.Button btnStart;
    }
}


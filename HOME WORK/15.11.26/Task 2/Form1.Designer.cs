namespace WindowsFormsApp3
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
            this.lbl1 = new System.Windows.Forms.Label();
            this.pb1 = new System.Windows.Forms.ProgressBar();
            this.btnRace = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.pb2 = new System.Windows.Forms.ProgressBar();
            this.pb3 = new System.Windows.Forms.ProgressBar();
            this.pb5 = new System.Windows.Forms.ProgressBar();
            this.pb4 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(134, 58);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(56, 13);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Лошадь 1";
            // 
            // pb1
            // 
            this.pb1.Location = new System.Drawing.Point(199, 48);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(200, 23);
            this.pb1.TabIndex = 1;
            // 
            // btnRace
            // 
            this.btnRace.Location = new System.Drawing.Point(324, 334);
            this.btnRace.Name = "btnRace";
            this.btnRace.Size = new System.Drawing.Size(75, 23);
            this.btnRace.TabIndex = 2;
            this.btnRace.Text = "Старт";
            this.btnRace.UseVisualStyleBackColor = true;
            this.btnRace.Click += new System.EventHandler(this.btnRace_Click);
            // 
            // lstResults
            // 
            this.lstResults.FormattingEnabled = true;
            this.lstResults.Location = new System.Drawing.Point(137, 262);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(120, 95);
            this.lstResults.TabIndex = 3;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(134, 81);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(56, 13);
            this.lbl2.TabIndex = 4;
            this.lbl2.Text = "Лошадь 2";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(134, 108);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(56, 13);
            this.lbl3.TabIndex = 5;
            this.lbl3.Text = "Лошадь 3";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(134, 145);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(56, 13);
            this.lbl4.TabIndex = 6;
            this.lbl4.Text = "Лошадь 4";
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Location = new System.Drawing.Point(134, 174);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(56, 13);
            this.lbl5.TabIndex = 7;
            this.lbl5.Text = "Лошадь 5";
            // 
            // pb2
            // 
            this.pb2.Location = new System.Drawing.Point(199, 77);
            this.pb2.Name = "pb2";
            this.pb2.Size = new System.Drawing.Size(200, 23);
            this.pb2.TabIndex = 8;
            // 
            // pb3
            // 
            this.pb3.Location = new System.Drawing.Point(199, 106);
            this.pb3.Name = "pb3";
            this.pb3.Size = new System.Drawing.Size(200, 23);
            this.pb3.TabIndex = 9;
            // 
            // pb5
            // 
            this.pb5.Location = new System.Drawing.Point(199, 164);
            this.pb5.Name = "pb5";
            this.pb5.Size = new System.Drawing.Size(200, 23);
            this.pb5.TabIndex = 10;
            // 
            // pb4
            // 
            this.pb4.Location = new System.Drawing.Point(199, 135);
            this.pb4.Name = "pb4";
            this.pb4.Size = new System.Drawing.Size(200, 23);
            this.pb4.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pb4);
            this.Controls.Add(this.pb5);
            this.Controls.Add(this.pb3);
            this.Controls.Add(this.pb2);
            this.Controls.Add(this.lbl5);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lstResults);
            this.Controls.Add(this.btnRace);
            this.Controls.Add(this.pb1);
            this.Controls.Add(this.lbl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ProgressBar pb1;
        private System.Windows.Forms.Button btnRace;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.ProgressBar pb2;
        private System.Windows.Forms.ProgressBar pb3;
        private System.Windows.Forms.ProgressBar pb5;
        private System.Windows.Forms.ProgressBar pb4;
    }
}


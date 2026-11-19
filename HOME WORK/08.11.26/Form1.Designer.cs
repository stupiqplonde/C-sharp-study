namespace WindowsFormsApp1
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
            this.txtPrimeFrom = new System.Windows.Forms.TextBox();
            this.txtPrimeTo = new System.Windows.Forms.TextBox();
            this.lstPrimes = new System.Windows.Forms.ListBox();
            this.btnPrimeStart = new System.Windows.Forms.Button();
            this.btnPrimeStop = new System.Windows.Forms.Button();
            this.btnPrimePause = new System.Windows.Forms.Button();
            this.btnPrimeResume = new System.Windows.Forms.Button();
            this.btnPrimeRestart = new System.Windows.Forms.Button();
            this.btnFibStart = new System.Windows.Forms.Button();
            this.btnFibStop = new System.Windows.Forms.Button();
            this.btnFibPause = new System.Windows.Forms.Button();
            this.btnFibResume = new System.Windows.Forms.Button();
            this.btnFibRestart = new System.Windows.Forms.Button();
            this.lstFib = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtPrimeFrom
            // 
            this.txtPrimeFrom.Location = new System.Drawing.Point(13, 13);
            this.txtPrimeFrom.Name = "txtPrimeFrom";
            this.txtPrimeFrom.Size = new System.Drawing.Size(100, 20);
            this.txtPrimeFrom.TabIndex = 0;
            this.txtPrimeFrom.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtPrimeTo
            // 
            this.txtPrimeTo.Location = new System.Drawing.Point(13, 39);
            this.txtPrimeTo.Name = "txtPrimeTo";
            this.txtPrimeTo.Size = new System.Drawing.Size(100, 20);
            this.txtPrimeTo.TabIndex = 1;
            // 
            // lstPrimes
            // 
            this.lstPrimes.FormattingEnabled = true;
            this.lstPrimes.Location = new System.Drawing.Point(138, 48);
            this.lstPrimes.Name = "lstPrimes";
            this.lstPrimes.Size = new System.Drawing.Size(120, 95);
            this.lstPrimes.TabIndex = 2;
            // 
            // btnPrimeStart
            // 
            this.btnPrimeStart.Location = new System.Drawing.Point(138, 13);
            this.btnPrimeStart.Name = "btnPrimeStart";
            this.btnPrimeStart.Size = new System.Drawing.Size(75, 23);
            this.btnPrimeStart.TabIndex = 3;
            this.btnPrimeStart.Text = "старт";
            this.btnPrimeStart.UseVisualStyleBackColor = true;
            this.btnPrimeStart.Click += new System.EventHandler(this.btnPrimeStart_Click);
            // 
            // btnPrimeStop
            // 
            this.btnPrimeStop.Location = new System.Drawing.Point(230, 13);
            this.btnPrimeStop.Name = "btnPrimeStop";
            this.btnPrimeStop.Size = new System.Drawing.Size(75, 23);
            this.btnPrimeStop.TabIndex = 4;
            this.btnPrimeStop.Text = "стоп";
            this.btnPrimeStop.UseVisualStyleBackColor = true;
            this.btnPrimeStop.Click += new System.EventHandler(this.btnPrimeStop_Click);
            // 
            // btnPrimePause
            // 
            this.btnPrimePause.Location = new System.Drawing.Point(311, 13);
            this.btnPrimePause.Name = "btnPrimePause";
            this.btnPrimePause.Size = new System.Drawing.Size(75, 23);
            this.btnPrimePause.TabIndex = 5;
            this.btnPrimePause.Text = "пауза";
            this.btnPrimePause.UseVisualStyleBackColor = true;
            this.btnPrimePause.Click += new System.EventHandler(this.btnPrimePause_Click);
            // 
            // btnPrimeResume
            // 
            this.btnPrimeResume.Location = new System.Drawing.Point(392, 13);
            this.btnPrimeResume.Name = "btnPrimeResume";
            this.btnPrimeResume.Size = new System.Drawing.Size(75, 23);
            this.btnPrimeResume.TabIndex = 6;
            this.btnPrimeResume.Text = "продолжить";
            this.btnPrimeResume.UseVisualStyleBackColor = true;
            this.btnPrimeResume.Click += new System.EventHandler(this.btnPrimeResume_Click);
            // 
            // btnPrimeRestart
            // 
            this.btnPrimeRestart.Location = new System.Drawing.Point(473, 13);
            this.btnPrimeRestart.Name = "btnPrimeRestart";
            this.btnPrimeRestart.Size = new System.Drawing.Size(75, 23);
            this.btnPrimeRestart.TabIndex = 7;
            this.btnPrimeRestart.Text = "рестарт";
            this.btnPrimeRestart.UseVisualStyleBackColor = true;
            this.btnPrimeRestart.Click += new System.EventHandler(this.btnPrimeRestart_Click);
            // 
            // btnFibStart
            // 
            this.btnFibStart.Location = new System.Drawing.Point(138, 255);
            this.btnFibStart.Name = "btnFibStart";
            this.btnFibStart.Size = new System.Drawing.Size(75, 23);
            this.btnFibStart.TabIndex = 8;
            this.btnFibStart.Text = "старт";
            this.btnFibStart.UseVisualStyleBackColor = true;
            this.btnFibStart.Click += new System.EventHandler(this.btnFibStart_Click);
            // 
            // btnFibStop
            // 
            this.btnFibStop.Location = new System.Drawing.Point(230, 255);
            this.btnFibStop.Name = "btnFibStop";
            this.btnFibStop.Size = new System.Drawing.Size(75, 23);
            this.btnFibStop.TabIndex = 9;
            this.btnFibStop.Text = "стоп";
            this.btnFibStop.UseVisualStyleBackColor = true;
            this.btnFibStop.Click += new System.EventHandler(this.btnFibStop_Click);
            // 
            // btnFibPause
            // 
            this.btnFibPause.Location = new System.Drawing.Point(311, 255);
            this.btnFibPause.Name = "btnFibPause";
            this.btnFibPause.Size = new System.Drawing.Size(75, 23);
            this.btnFibPause.TabIndex = 10;
            this.btnFibPause.Text = "пауза";
            this.btnFibPause.UseVisualStyleBackColor = true;
            this.btnFibPause.Click += new System.EventHandler(this.btnFibPause_Click);
            // 
            // btnFibResume
            // 
            this.btnFibResume.Location = new System.Drawing.Point(392, 255);
            this.btnFibResume.Name = "btnFibResume";
            this.btnFibResume.Size = new System.Drawing.Size(75, 23);
            this.btnFibResume.TabIndex = 11;
            this.btnFibResume.Text = "продолжить";
            this.btnFibResume.UseVisualStyleBackColor = true;
            this.btnFibResume.Click += new System.EventHandler(this.btnFibResume_Click);
            // 
            // btnFibRestart
            // 
            this.btnFibRestart.Location = new System.Drawing.Point(473, 255);
            this.btnFibRestart.Name = "btnFibRestart";
            this.btnFibRestart.Size = new System.Drawing.Size(75, 23);
            this.btnFibRestart.TabIndex = 12;
            this.btnFibRestart.Text = "рестрат";
            this.btnFibRestart.UseVisualStyleBackColor = true;
            this.btnFibRestart.Click += new System.EventHandler(this.btnFibRestart_Click);
            // 
            // lstFib
            // 
            this.lstFib.FormattingEnabled = true;
            this.lstFib.Location = new System.Drawing.Point(138, 302);
            this.lstFib.Name = "lstFib";
            this.lstFib.Size = new System.Drawing.Size(120, 95);
            this.lstFib.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstFib);
            this.Controls.Add(this.btnFibRestart);
            this.Controls.Add(this.btnFibResume);
            this.Controls.Add(this.btnFibPause);
            this.Controls.Add(this.btnFibStop);
            this.Controls.Add(this.btnFibStart);
            this.Controls.Add(this.btnPrimeRestart);
            this.Controls.Add(this.btnPrimeResume);
            this.Controls.Add(this.btnPrimePause);
            this.Controls.Add(this.btnPrimeStop);
            this.Controls.Add(this.btnPrimeStart);
            this.Controls.Add(this.lstPrimes);
            this.Controls.Add(this.txtPrimeTo);
            this.Controls.Add(this.txtPrimeFrom);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPrimeFrom;
        private System.Windows.Forms.TextBox txtPrimeTo;
        private System.Windows.Forms.ListBox lstPrimes;
        private System.Windows.Forms.Button btnPrimeStart;
        private System.Windows.Forms.Button btnPrimeStop;
        private System.Windows.Forms.Button btnPrimePause;
        private System.Windows.Forms.Button btnPrimeResume;
        private System.Windows.Forms.Button btnPrimeRestart;
        private System.Windows.Forms.Button btnFibStart;
        private System.Windows.Forms.Button btnFibStop;
        private System.Windows.Forms.Button btnFibPause;
        private System.Windows.Forms.Button btnFibResume;
        private System.Windows.Forms.Button btnFibRestart;
        private System.Windows.Forms.ListBox lstFib;
    }
}


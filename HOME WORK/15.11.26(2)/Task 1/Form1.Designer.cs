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
            this.btnStart = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.numPairs = new System.Windows.Forms.NumericUpDown();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numPairs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(283, 108);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Старт";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(272, 172);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(100, 96);
            this.rtbLog.TabIndex = 1;
            this.rtbLog.Text = "Лог";
            // 
            // numPairs
            // 
            this.numPairs.Location = new System.Drawing.Point(33, 148);
            this.numPairs.Name = "numPairs";
            this.numPairs.Size = new System.Drawing.Size(120, 20);
            this.numPairs.TabIndex = 2;
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(364, 108);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(118, 43);
            this.btnOpenFolder.TabIndex = 3;
            this.btnOpenFolder.Text = "открыть папку с результатами";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(511, 387);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.numPairs);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numPairs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.NumericUpDown numPairs;
        private System.Windows.Forms.Button btnOpenFolder;
    }
}


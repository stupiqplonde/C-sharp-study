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
            this.txtDir = new System.Windows.Forms.TextBox();
            this.btnBrowseDir = new System.Windows.Forms.Button();
            this.rtbReport = new System.Windows.Forms.RichTextBox();
            this.btnSearchDir = new System.Windows.Forms.Button();
            this.txtWord = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(140, 199);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(100, 20);
            this.txtDir.TabIndex = 0;
            // 
            // btnBrowseDir
            // 
            this.btnBrowseDir.Location = new System.Drawing.Point(246, 199);
            this.btnBrowseDir.Name = "btnBrowseDir";
            this.btnBrowseDir.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseDir.TabIndex = 1;
            this.btnBrowseDir.Text = "Обзор";
            this.btnBrowseDir.UseVisualStyleBackColor = true;
            this.btnBrowseDir.Click += new System.EventHandler(this.btnBrowseDir_Click);
            // 
            // rtbReport
            // 
            this.rtbReport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbReport.Location = new System.Drawing.Point(0, 292);
            this.rtbReport.Name = "rtbReport";
            this.rtbReport.Size = new System.Drawing.Size(690, 96);
            this.rtbReport.TabIndex = 2;
            this.rtbReport.Text = "";
            // 
            // btnSearchDir
            // 
            this.btnSearchDir.Location = new System.Drawing.Point(246, 223);
            this.btnSearchDir.Name = "btnSearchDir";
            this.btnSearchDir.Size = new System.Drawing.Size(75, 23);
            this.btnSearchDir.TabIndex = 3;
            this.btnSearchDir.Text = "Искать";
            this.btnSearchDir.UseVisualStyleBackColor = true;
            this.btnSearchDir.Click += new System.EventHandler(this.btnSearchDir_Click);
            // 
            // txtWord
            // 
            this.txtWord.Location = new System.Drawing.Point(140, 225);
            this.txtWord.Name = "txtWord";
            this.txtWord.Size = new System.Drawing.Size(100, 20);
            this.txtWord.TabIndex = 4;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(690, 388);
            this.Controls.Add(this.txtWord);
            this.Controls.Add(this.btnSearchDir);
            this.Controls.Add(this.rtbReport);
            this.Controls.Add(this.btnBrowseDir);
            this.Controls.Add(this.txtDir);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Button btnBrowseDir;
        private System.Windows.Forms.RichTextBox rtbReport;
        private System.Windows.Forms.Button btnSearchDir;
        private System.Windows.Forms.TextBox txtWord;
    }
}


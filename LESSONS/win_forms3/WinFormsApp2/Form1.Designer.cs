namespace WinFormsApp2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            progressBar1 = new ProgressBar();
            timer1 = new System.Windows.Forms.Timer(components);
            pictureBoxCongratulations = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCongratulations).BeginInit();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 12);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(400, 23);
            progressBar1.TabIndex = 0;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            // 
            // pictureBoxCongratulations
            // 
            pictureBoxCongratulations.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxCongratulations.Location = new Point(12, 50);
            pictureBoxCongratulations.Name = "pictureBoxCongratulations";
            pictureBoxCongratulations.Size = new Size(400, 200);
            pictureBoxCongratulations.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxCongratulations.TabIndex = 1;
            pictureBoxCongratulations.TabStop = false;
            pictureBoxCongratulations.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(424, 262);
            Controls.Add(pictureBoxCongratulations);
            Controls.Add(progressBar1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Прогресс";
            ((System.ComponentModel.ISupportInitialize)pictureBoxCongratulations).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pictureBoxCongratulations;
    }
}

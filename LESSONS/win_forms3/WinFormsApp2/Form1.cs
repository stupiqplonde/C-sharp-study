namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            if (progressBar1.Value >= progressBar1.Maximum)
            {
                return;
            }

            progressBar1.Value++;

            if (progressBar1.Value >= progressBar1.Maximum)
            {
                timer1.Stop();
                ShowCongratulations();
            }
        }

        private void ShowCongratulations()
        {
            pictureBoxCongratulations.Image?.Dispose();
            pictureBoxCongratulations.Image = CreateCongratulationsImage();
            pictureBoxCongratulations.Visible = true;
        }

        private static Bitmap CreateCongratulationsImage()
        {
            var bitmap = new Bitmap(380, 180);
            using var g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.FromArgb(255, 255, 215, 0));

            using var borderPen = new Pen(Color.DarkGreen, 4);
            g.DrawRectangle(borderPen, 2, 2, bitmap.Width - 5, bitmap.Height - 5);

            using var textBrush = new SolidBrush(Color.DarkGreen);
            using var font = new Font("Segoe UI", 32, FontStyle.Bold);
            const string text = "Поздравляем!";
            SizeF size = g.MeasureString(text, font);
            g.DrawString(
                text,
                font,
                textBrush,
                (bitmap.Width - size.Width) / 2,
                (bitmap.Height - size.Height) / 2);

            return bitmap;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            pictureBoxCongratulations.Image?.Dispose();
            base.OnFormClosed(e);
        }
    }
}

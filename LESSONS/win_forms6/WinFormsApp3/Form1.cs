namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private Image? img;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img.png");
            using var temp = Image.FromFile(path);
            img = new Bitmap(temp);
            Invalidate();
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            if (img == null)
            {
                return;
            }

            e.Graphics.DrawImage(img, 20, 20, img.Width, img.Height);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            img?.Dispose();
            base.OnFormClosed(e);
        }
    }
}

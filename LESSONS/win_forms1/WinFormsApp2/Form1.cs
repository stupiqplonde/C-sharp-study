namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private static readonly Color[] Colors =
        [
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.Indigo,
            Color.Purple,
            Color.Magenta,
            Color.Cyan,
            Color.Brown
        ];

        private int _colorIndex;

        public Form1()
        {
            InitializeComponent();
            label1.ForeColor = Colors[_colorIndex];
            timer1.Start();
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            _colorIndex = (_colorIndex + 1) % Colors.Length;
            label1.ForeColor = Colors[_colorIndex];
        }
    }
}

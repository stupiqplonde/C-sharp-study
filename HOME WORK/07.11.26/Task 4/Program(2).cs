using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern IntPtr FindWindow(
        string? className,
        string? windowName
    );

    [DllImport("user32.dll")]
    static extern bool EnumChildWindows(
        IntPtr hWnd,
        EnumWindowsProc callback,
        IntPtr lParam
    );

    delegate bool EnumWindowsProc(
        IntPtr hWnd,
        IntPtr lParam
    );

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern int GetClassName(
        IntPtr hWnd,
        StringBuilder className,
        int maxCount
    );

    [DllImport("user32.dll")]
    static extern int GetWindowLong(
        IntPtr hWnd,
        int index
    );

    [DllImport("user32.dll")]
    static extern int SetWindowLong(
        IntPtr hWnd,
        int index,
        int value
    );

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(
        IntPtr hWnd,
        IntPtr insertAfter,
        int x,
        int y,
        int width,
        int height,
        uint flags
    );

    const int GWL_STYLE = -16;
    const int ES_READONLY = 0x0800;
    const int WS_DISABLED = 0x08000000;
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOMOVE = 0x0002;
    const uint SWP_NOZORDER = 0x0004;
    const uint SWP_FRAMECHANGED = 0x0020;

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ControllerForm());
    }

    class ControllerForm : Form
    {
        Button button;

        public ControllerForm()
        {
            Text = "Управление стилями";
            Width = 350;
            Height = 180;

            button = new Button();
            button.Text = "Изменить стили";
            button.SetBounds(50, 40, 220, 45);

            button.Click += ChangeStyles;

            Controls.Add(button);
        }

        void ChangeStyles(object? sender, EventArgs e)
        {
            IntPtr hwnd = FindWindow(null, "TargetApp");

            if (hwnd == IntPtr.Zero)
            {
                MessageBox.Show("Первое приложение не найдено");
                return;
            }

            EnumChildWindows(hwnd, ChangeControlStyle, IntPtr.Zero);

            MessageBox.Show("Стили элементов управления изменены");
        }

        bool ChangeControlStyle(IntPtr hwnd, IntPtr lParam)
        {
            StringBuilder className = new StringBuilder(256);

            GetClassName(hwnd, className, className.Capacity);

            string name = className.ToString();

            int style = GetWindowLong(hwnd, GWL_STYLE);

            if (name.Contains("EDIT"))
            {
                style |= ES_READONLY;

                SetWindowLong(hwnd, GWL_STYLE, style);
            }

            if (name.Contains("BUTTON"))
            {
                style |= WS_DISABLED;

                SetWindowLong(hwnd, GWL_STYLE, style);
            }

            SetWindowPos(
                hwnd,
                IntPtr.Zero,
                0, 0, 0, 0,
                SWP_NOMOVE |
                SWP_NOSIZE |
                SWP_NOZORDER |
                SWP_FRAMECHANGED
            );

            return true;
        }
    }
}
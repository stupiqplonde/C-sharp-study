using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern IntPtr FindWindow(
        string? lpClassName,
        string? lpWindowName
    );

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern IntPtr SendMessage(
        IntPtr hWnd,
        uint Msg,
        IntPtr wParam,
        string lParam
    );

    [DllImport("user32.dll")]
    static extern IntPtr SendMessage(
        IntPtr hWnd,
        uint Msg,
        IntPtr wParam,
        IntPtr lParam
    );

    const uint WM_SETTEXT = 0x000C;
    const uint WM_CLOSE = 0x0010;

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MainForm());
    }

    class MainForm : Form
    {
        TextBox input;
        Button changeButton;
        Button closeButton;

        public MainForm()
        {
            Text = "Мое приложение";
            Width = 400;
            Height = 220;

            input = new TextBox();
            input.SetBounds(30, 30, 320, 25);

            changeButton = new Button();
            changeButton.Text = "Изменить заголовок";
            changeButton.SetBounds(30, 75, 320, 35);
            changeButton.Click += ChangeTitle;

            closeButton = new Button();
            closeButton.Text = "Закрыть окно";
            closeButton.SetBounds(30, 120, 320, 35);
            closeButton.Click += CloseWindow;

            Controls.Add(input);
            Controls.Add(changeButton);
            Controls.Add(closeButton);
        }

        void ChangeTitle(object? sender, EventArgs e)
        {
            IntPtr hwnd = FindWindow(null, Text);

            if (hwnd != IntPtr.Zero)
            {
                string newTitle = input.Text;

                SendMessage(
                    hwnd,
                    WM_SETTEXT,
                    IntPtr.Zero,
                    newTitle
                );

                Text = newTitle;
            }
            else
            {
                MessageBox.Show("Окно не обнаруженг");
            }
        }

        void CloseWindow(object? sender, EventArgs e)
        {
            IntPtr hwnd = FindWindow(null, Text);

            if (hwnd != IntPtr.Zero)
            {
                SendMessage(
                    hwnd,
                    WM_CLOSE,
                    IntPtr.Zero,
                    IntPtr.Zero
                );
            }
            else
            {
                MessageBox.Show("Окно не обнаружено");
            }
        }
    }
}
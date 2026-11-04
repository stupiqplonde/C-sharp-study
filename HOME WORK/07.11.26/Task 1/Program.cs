using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern int MessageBox(
        IntPtr hWnd,
        string text,
        string caption,
        uint type
    );

    static void Main()
    {
        MessageBox(
            IntPtr.Zero,
            "Ковалев Михаил РПО-2",
            "обо мне",
            0
        );

        MessageBox(
            IntPtr.Zero,
            "разработка программного обеспечения",
            "Моя специальность",
            0
        );

        MessageBox(
            IntPtr.Zero,
            "люблю базы данных",
            "Мои интересы",
            0
        );

        MessageBox(
            IntPtr.Zero,
            "Моя цель — стать квалифицированным программистом",
            "Моя цель",
            0
        );
    }
}
using System;
using System.Windows.Forms;

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MainForm());
    }
}

class MainForm : Form
{
    public MainForm()
    {
        Text = "TargetApp";
        Width = 400;
        Height = 250;

        Label label = new Label();
        label.Text = "Введите текст:";
        label.SetBounds(30, 25, 200, 25);

        TextBox textBox = new TextBox();
        textBox.SetBounds(30, 55, 300, 25);

        Button button = new Button();
        button.Text = "Отправить";
        button.SetBounds(30, 100, 150, 35);

        Controls.Add(label);
        Controls.Add(textBox);
        Controls.Add(button);
    }
}
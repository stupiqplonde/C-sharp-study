using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MyLibrary; 

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnSquareArea_Click(object sender, EventArgs e)
        {
            try
            {
                double side = double.Parse(txtSquareSide.Text);
                lblGeoResult.Text = "Площадь квадрата: " + GeometryHelper.SquareArea(side);
            }
            catch { MessageBox.Show("Введите корректное число в поле стороны квадрата"); }
        }

        private void btnRectArea_Click(object sender, EventArgs e)
        {
            try
            {
                double w = double.Parse(txtRectW.Text);
                double h = double.Parse(txtRectH.Text);
                lblGeoResult.Text = "Площадь прямоугольника: " + GeometryHelper.RectangleArea(w, h);
            }
            catch { MessageBox.Show("Введите корректные числа для прямоугольника"); }
        }

        private void btnTriangleArea_Click(object sender, EventArgs e)
        {
            try
            {
                double b = double.Parse(txtTriangleBase.Text);
                double h = double.Parse(txtTriangleH.Text);
                lblGeoResult.Text = "Площадь треугольника: " + GeometryHelper.TriangleArea(b, h);
            }
            catch { MessageBox.Show("Введите корректные числа для треугольника"); }
        }

        private void btnPalindrome_Click(object sender, EventArgs e)
        {
            bool isPal = TextHelper.IsPalindrome(txtInputText.Text);
            lblTextResult.Text = isPal ? "Это палиндром!" : "Это НЕ палиндром.";
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            lblTextResult.Text = TextHelper.ReverseString(txtInputText.Text);
        }

        private void btnCountSentences_Click(object sender, EventArgs e)
        {
            int count = TextHelper.CountSentences(txtInputText.Text);
            lblTextResult.Text = "Количество предложений: " + count;
        }

        private void btnValidateContact_Click(object sender, EventArgs e)
        {
            string result = "";
            if (!ContactValidator.IsValidName(txtName.Text)) result += "Ошибка: ФИО (только буквы).\n";
            if (!ContactValidator.IsValidAge(txtAge.Text)) result += "Ошибка: Возраст (только цифры).\n";
            if (!ContactValidator.IsValidPhone(txtPhone.Text)) result += "Ошибка: Телефон.\n";
            if (!ContactValidator.IsValidEmail(txtEmail.Text)) result += "Ошибка: Email.\n";

            if (result == "") result = "Все данные корректны!";
            lblContactResult.Text = result;
        }

        private void btnPickFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txtFilePath.Text = openFileDialog1.FileName;
        }

        private void btnPickFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                txtFilePath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnCopyFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilePath.Text)) return;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FileHelper.CopyFile(txtFilePath.Text, sfd.FileName);
                    lblFileResult.Text = "Файл скопирован!";
                }
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilePath.Text)) return;
            FileHelper.DeleteFile(txtFilePath.Text);
            lblFileResult.Text = "Файл удален (если существовал).";
        }

        private void btnDeleteByMask_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilePath.Text)) return;
            string mask = ShowInputDialog("Введите маску для удаления:", "*.txt");
            if (!string.IsNullOrWhiteSpace(mask))
            {
                FileHelper.DeleteFilesByMask(txtFilePath.Text, mask);
                lblFileResult.Text = "Удаление по маске выполнено.";
            }
        }

        private void btnMoveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilePath.Text)) return;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FileHelper.MoveFile(txtFilePath.Text, sfd.FileName);
                    lblFileResult.Text = "Файл перенесен!";
                }
            }
        }

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchPath.Text) || string.IsNullOrWhiteSpace(txtSearchWord.Text)) return;
            string reportPath = Path.Combine(Path.GetDirectoryName(txtSearchPath.Text), "report_file.txt");
            int count = FileHelper.SearchWordInFile(txtSearchPath.Text, txtSearchWord.Text, reportPath);
            lblSearchResult.Text = "Найдено: " + count + ". Отчет: " + reportPath;
        }

        private void btnSearchFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchPath.Text) || string.IsNullOrWhiteSpace(txtSearchWord.Text)) return;
            string reportPath = Path.Combine(txtSearchPath.Text, "report_folder.txt");
            int count = FileHelper.SearchWordInFolder(txtSearchPath.Text, txtSearchWord.Text, reportPath);
            lblSearchResult.Text = "Найдено: " + count + ". Отчет: " + reportPath;
        }

        private string ShowInputDialog(string prompt, string defaultValue)
        {
            Form promptForm = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = prompt,
                StartPosition = FormStartPosition.CenterParent
            };
            TextBox textBox = new TextBox() { Left = 20, Top = 20, Width = 340, Text = defaultValue };
            Button confirmation = new Button() { Text = "OK", Left = 280, Width = 80, Top = 60, DialogResult = DialogResult.OK };

            promptForm.Controls.Add(textBox);
            promptForm.Controls.Add(confirmation);
            promptForm.AcceptButton = confirmation;

            return promptForm.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
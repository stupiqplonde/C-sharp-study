namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private readonly HashSet<string> _bannedItems = new(StringComparer.Ordinal);

        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object? sender, EventArgs e)
        {
            string text = textBoxInput.Text.Trim();
            if (text.Length == 0)
            {
                return;
            }

            if (_bannedItems.Contains(text))
            {
                MessageBox.Show(
                    "Этот элемент был удалён и больше не может быть добавлен.",
                    "Добавление запрещено",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (listBox1.Items.Contains(text))
            {
                MessageBox.Show(
                    "Такой элемент уже есть в списке.",
                    "Дубликат",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            listBox1.Items.Add(text);
            textBoxInput.Clear();
            textBoxInput.Focus();
        }

        private void ButtonDelete_Click(object? sender, EventArgs e)
        {
            if (listBox1.SelectedItem is not string selected)
            {
                MessageBox.Show(
                    "Выберите элемент для удаления.",
                    "Ничего не выбрано",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            _bannedItems.Add(selected);
            listBox1.Items.Remove(selected);
        }
    }
}

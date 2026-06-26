namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FillListView();
        }

        private void FillListView()
        {
            listView.Items.Clear();
            listView.Items.Add(new ListViewItem(new[] { "Иван", "25", "Москва" }));
            listView.Items.Add(new ListViewItem(new[] { "Мария", "30", "Санкт-Петербург" }));
            listView.Items.Add(new ListViewItem(new[] { "Пётр", "22", "Казань" }));
            listView.Items.Add(new ListViewItem(new[] { "Анна", "28", "Новосибирск" }));
            listView.Items.Add(new ListViewItem(new[] { "Олег", "35", "Екатеринбург" }));
        }

        private void ButtonTransfer_Click(object? sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show(
                    "Выберите строку в списке.",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            ListViewItem item = listView.SelectedItems[0];
            label1.Text = item.Text;
            label2.Text = item.SubItems.Count > 1 ? item.SubItems[1].Text : "";
            label3.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";

            listView.Items.Remove(item);
        }
    }
}

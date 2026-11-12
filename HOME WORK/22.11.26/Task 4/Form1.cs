using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Task4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string folderPath = folderBrowserDialog1.SelectedPath;

                    var resumes = Directory.GetFiles(folderPath, "*.txt")
                        .AsParallel()
                        .Select(file => ParseResume(file))
                        .ToList();

                    if (resumes.Count == 0)
                    {
                        txtResult.Text = "В выбранной папке нет .txt файлов.";
                        return;
                    }

                    var sb = new StringBuilder();
                    sb.AppendLine($"Успешно загружено резюме: {resumes.Count}");
                    sb.AppendLine(new string('-', 30));

                    var mostExp = resumes.OrderByDescending(r => r.Experience).First();
                    sb.AppendLine($"[Самый опытный]: {mostExp.Name} (Опыт: {mostExp.Experience} лет)");

                    var leastExp = resumes.OrderBy(r => r.Experience).First();
                    sb.AppendLine($"[Самый неопытный]: {leastExp.Name} (Опыт: {leastExp.Experience} лет)");

                    var lowestSalary = resumes.OrderBy(r => r.Salary).First();
                    sb.AppendLine($"[Мин. зарплата]: {lowestSalary.Name} ({lowestSalary.Salary} руб.)");

                    var highestSalary = resumes.OrderByDescending(r => r.Salary).First();
                    sb.AppendLine($"[Макс. зарплата]: {highestSalary.Name} ({highestSalary.Salary} руб.)");

                    sb.AppendLine(new string('-', 30));
                    sb.AppendLine("[Кандидаты из одного города]:");

                    var sameCityGroups = resumes.GroupBy(r => r.City).Where(g => g.Count() > 1);
                    bool hasSameCity = false;
                    foreach (var group in sameCityGroups)
                    {
                        hasSameCity = true;
                        sb.AppendLine($"Город {group.Key}: {string.Join(", ", group.Select(r => r.Name))}");
                    }
                    if (!hasSameCity) sb.AppendLine("Совпадений по городам нет.");

                    txtResult.Text = sb.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка обработки: " + ex.Message);
                }
            }
        }

        private class Resume
        {
            public string Name { get; set; }
            public string City { get; set; }
            public int Experience { get; set; }
            public int Salary { get; set; }
        }

        private Resume ParseResume(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var resume = new Resume();
            foreach (var line in lines)
            {
                if (line.StartsWith("Имя:")) resume.Name = line.Substring(4).Trim();
                else if (line.StartsWith("Город:")) resume.City = line.Substring(6).Trim();
                else if (line.StartsWith("Опыт:")) resume.Experience = int.Parse(line.Substring(5).Trim());
                else if (line.StartsWith("Зарплата:")) resume.Salary = int.Parse(line.Substring(9).Trim());
            }
            return resume;
        }
    }
}
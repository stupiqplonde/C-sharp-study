namespace WinFormsApp2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            listView = new ListView();
            columnName = new ColumnHeader();
            columnAge = new ColumnHeader();
            columnCity = new ColumnHeader();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            buttonTransfer = new Button();
            SuspendLayout();
            // 
            // listView
            // 
            listView.Columns.AddRange(new ColumnHeader[] { columnName, columnAge, columnCity });
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Location = new Point(12, 12);
            listView.Name = "listView";
            listView.Size = new Size(460, 200);
            listView.TabIndex = 0;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            // 
            // columnName
            // 
            columnName.Text = "Имя";
            columnName.Width = 150;
            // 
            // columnAge
            // 
            columnAge.Text = "Возраст";
            columnAge.Width = 100;
            // 
            // columnCity
            // 
            columnCity.Text = "Город";
            columnCity.Width = 180;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 225);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 1;
            label1.Text = "Имя:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 255);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 2;
            label2.Text = "Возраст:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 275);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 3;
            label3.Text = "Город:";
            // 
            // buttonTransfer
            // 
            buttonTransfer.Location = new Point(12, 305);
            buttonTransfer.Name = "buttonTransfer";
            buttonTransfer.Size = new Size(200, 35);
            buttonTransfer.TabIndex = 4;
            buttonTransfer.Text = "Перенести строку";
            buttonTransfer.UseVisualStyleBackColor = true;
            buttonTransfer.Click += ButtonTransfer_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(514, 352);
            Controls.Add(buttonTransfer);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ListView";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView;
        private ColumnHeader columnName;
        private ColumnHeader columnAge;
        private ColumnHeader columnCity;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button buttonTransfer;
    }
}

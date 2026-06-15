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
            textBoxInput = new TextBox();
            listBox1 = new ListBox();
            buttonAdd = new Button();
            buttonDelete = new Button();
            labelInput = new Label();
            SuspendLayout();
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(12, 27);
            textBoxInput.Name = "textBoxInput";
            textBoxInput.Size = new Size(260, 23);
            textBoxInput.TabIndex = 0;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 56);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(260, 199);
            listBox1.TabIndex = 1;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(12, 261);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(125, 30);
            buttonAdd.TabIndex = 2;
            buttonAdd.Text = "Добавить";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += ButtonAdd_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(147, 261);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(125, 30);
            buttonDelete.TabIndex = 3;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += ButtonDelete_Click;
            // 
            // labelInput
            // 
            labelInput.AutoSize = true;
            labelInput.Location = new Point(12, 9);
            labelInput.Name = "labelInput";
            labelInput.Size = new Size(117, 15);
            labelInput.TabIndex = 4;
            labelInput.Text = "Новый элемент";
            // 
            // Form1
            // 
            AcceptButton = buttonAdd;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 303);
            Controls.Add(labelInput);
            Controls.Add(buttonDelete);
            Controls.Add(buttonAdd);
            Controls.Add(listBox1);
            Controls.Add(textBoxInput);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Список элементов";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxInput;
        private ListBox listBox1;
        private Button buttonAdd;
        private Button buttonDelete;
        private Label labelInput;
    }
}

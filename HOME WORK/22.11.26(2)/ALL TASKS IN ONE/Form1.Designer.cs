namespace TestApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblGeoResult = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSquareSide = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.lblContactResult = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.lblFileResult = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnPickFolder = new System.Windows.Forms.Button();
            this.btnPickFile = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.txtSearchWord = new System.Windows.Forms.TextBox();
            this.txtSearchPath = new System.Windows.Forms.TextBox();
            this.lblTextResult = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtInputText = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtTriangleH = new System.Windows.Forms.TextBox();
            this.txtTriangleBase = new System.Windows.Forms.TextBox();
            this.txtRectH = new System.Windows.Forms.TextBox();
            this.txtRectW = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(44, 116);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(457, 295);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtRectW);
            this.tabPage1.Controls.Add(this.txtRectH);
            this.tabPage1.Controls.Add(this.txtTriangleBase);
            this.tabPage1.Controls.Add(this.txtTriangleH);
            this.tabPage1.Controls.Add(this.lblGeoResult);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.txtSquareSide);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(449, 269);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Геометрия";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblGeoResult
            // 
            this.lblGeoResult.AutoSize = true;
            this.lblGeoResult.Location = new System.Drawing.Point(71, 189);
            this.lblGeoResult.Name = "lblGeoResult";
            this.lblGeoResult.Size = new System.Drawing.Size(0, 13);
            this.lblGeoResult.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(74, 163);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(218, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Площадь треугольника";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(74, 134);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Площадь прямоугольника";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(74, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Площадь квадрата";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtSquareSide
            // 
            this.txtSquareSide.Location = new System.Drawing.Point(151, 19);
            this.txtSquareSide.Name = "txtSquareSide";
            this.txtSquareSide.Size = new System.Drawing.Size(100, 20);
            this.txtSquareSide.TabIndex = 1;
            this.txtSquareSide.Text = "Сторона квадрата";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(449, 269);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Текст";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lblContactResult);
            this.tabPage5.Controls.Add(this.button7);
            this.tabPage5.Controls.Add(this.txtEmail);
            this.tabPage5.Controls.Add(this.txtPhone);
            this.tabPage5.Controls.Add(this.txtAge);
            this.tabPage5.Controls.Add(this.txtName);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Controls.Add(this.label4);
            this.tabPage5.Controls.Add(this.label3);
            this.tabPage5.Controls.Add(this.label2);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(449, 269);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Контакты";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // lblContactResult
            // 
            this.lblContactResult.AutoSize = true;
            this.lblContactResult.Location = new System.Drawing.Point(224, 146);
            this.lblContactResult.Name = "lblContactResult";
            this.lblContactResult.Size = new System.Drawing.Size(35, 13);
            this.lblContactResult.TabIndex = 9;
            this.lblContactResult.Text = "label6";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(96, 136);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 8;
            this.button7.Text = "Проверить";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(181, 91);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 20);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.Text = "Email ";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(181, 67);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(100, 20);
            this.txtPhone.TabIndex = 6;
            this.txtPhone.Text = "Телефон ";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(181, 40);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(100, 20);
            this.txtAge.TabIndex = 5;
            this.txtAge.Text = "Возраст ";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(181, 14);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 4;
            this.txtName.Text = "ФИО ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(93, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(93, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(93, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.lblFileResult);
            this.tabPage6.Controls.Add(this.button11);
            this.tabPage6.Controls.Add(this.button10);
            this.tabPage6.Controls.Add(this.button9);
            this.tabPage6.Controls.Add(this.button8);
            this.tabPage6.Controls.Add(this.txtFilePath);
            this.tabPage6.Controls.Add(this.btnPickFolder);
            this.tabPage6.Controls.Add(this.btnPickFile);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(449, 269);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Файлы";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // lblFileResult
            // 
            this.lblFileResult.AutoSize = true;
            this.lblFileResult.Location = new System.Drawing.Point(35, 144);
            this.lblFileResult.Name = "lblFileResult";
            this.lblFileResult.Size = new System.Drawing.Size(35, 13);
            this.lblFileResult.TabIndex = 7;
            this.lblFileResult.Text = "label6";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(319, 114);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 6;
            this.button11.Text = "Перенести";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(197, 114);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(116, 23);
            this.button10.TabIndex = 5;
            this.button10.Text = "Удалить по маске";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(116, 114);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 4;
            this.button9.Text = "Удалить";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(35, 114);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 3;
            this.button8.Text = "Копировать";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(35, 87);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(100, 20);
            this.txtFilePath.TabIndex = 2;
            // 
            // btnPickFolder
            // 
            this.btnPickFolder.Location = new System.Drawing.Point(169, 37);
            this.btnPickFolder.Name = "btnPickFolder";
            this.btnPickFolder.Size = new System.Drawing.Size(112, 23);
            this.btnPickFolder.TabIndex = 1;
            this.btnPickFolder.Text = "Выбрать папку...";
            this.btnPickFolder.UseVisualStyleBackColor = true;
            // 
            // btnPickFile
            // 
            this.btnPickFile.Location = new System.Drawing.Point(35, 37);
            this.btnPickFile.Name = "btnPickFile";
            this.btnPickFile.Size = new System.Drawing.Size(112, 23);
            this.btnPickFile.TabIndex = 0;
            this.btnPickFile.Text = "Выбрать файл...";
            this.btnPickFile.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.lblSearchResult);
            this.tabPage7.Controls.Add(this.button13);
            this.tabPage7.Controls.Add(this.button12);
            this.tabPage7.Controls.Add(this.txtSearchWord);
            this.tabPage7.Controls.Add(this.txtSearchPath);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(449, 269);
            this.tabPage7.TabIndex = 4;
            this.tabPage7.Text = "Поиск слов";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.AutoSize = true;
            this.lblSearchResult.Location = new System.Drawing.Point(38, 114);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Size = new System.Drawing.Size(35, 13);
            this.lblSearchResult.TabIndex = 4;
            this.lblSearchResult.Text = "label6";
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(191, 70);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(117, 23);
            this.button13.TabIndex = 3;
            this.button13.Text = "Искать в папке";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(47, 70);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(113, 23);
            this.button12.TabIndex = 2;
            this.button12.Text = "Искать в файле";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // txtSearchWord
            // 
            this.txtSearchWord.Location = new System.Drawing.Point(166, 16);
            this.txtSearchWord.Name = "txtSearchWord";
            this.txtSearchWord.Size = new System.Drawing.Size(100, 20);
            this.txtSearchWord.TabIndex = 1;
            // 
            // txtSearchPath
            // 
            this.txtSearchPath.Location = new System.Drawing.Point(38, 16);
            this.txtSearchPath.Name = "txtSearchPath";
            this.txtSearchPath.Size = new System.Drawing.Size(100, 20);
            this.txtSearchPath.TabIndex = 0;
            // 
            // lblTextResult
            // 
            this.lblTextResult.AutoSize = true;
            this.lblTextResult.Location = new System.Drawing.Point(244, 124);
            this.lblTextResult.Name = "lblTextResult";
            this.lblTextResult.Size = new System.Drawing.Size(0, 13);
            this.lblTextResult.TabIndex = 4;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(46, 115);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(146, 23);
            this.button6.TabIndex = 3;
            this.button6.Text = "Кол-во предложений";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(46, 86);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(116, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "Перевернуть";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(46, 57);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Палиндром?";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // txtInputText
            // 
            this.txtInputText.Location = new System.Drawing.Point(180, 39);
            this.txtInputText.Multiline = true;
            this.txtInputText.Name = "txtInputText";
            this.txtInputText.Size = new System.Drawing.Size(100, 20);
            this.txtInputText.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtTriangleH
            // 
            this.txtTriangleH.Location = new System.Drawing.Point(287, 71);
            this.txtTriangleH.Name = "txtTriangleH";
            this.txtTriangleH.Size = new System.Drawing.Size(100, 20);
            this.txtTriangleH.TabIndex = 6;
            this.txtTriangleH.Text = "Высота треугольника";
            // 
            // txtTriangleBase
            // 
            this.txtTriangleBase.Location = new System.Drawing.Point(151, 45);
            this.txtTriangleBase.Name = "txtTriangleBase";
            this.txtTriangleBase.Size = new System.Drawing.Size(100, 20);
            this.txtTriangleBase.TabIndex = 7;
            this.txtTriangleBase.Text = "Основание треугольника";
            // 
            // txtRectH
            // 
            this.txtRectH.Location = new System.Drawing.Point(287, 45);
            this.txtRectH.Name = "txtRectH";
            this.txtRectH.Size = new System.Drawing.Size(100, 20);
            this.txtRectH.TabIndex = 8;
            this.txtRectH.Text = "Высота прямоугольника";
            // 
            // txtRectW
            // 
            this.txtRectW.Location = new System.Drawing.Point(287, 19);
            this.txtRectW.Name = "txtRectW";
            this.txtRectW.Size = new System.Drawing.Size(100, 20);
            this.txtRectW.TabIndex = 9;
            this.txtRectW.Text = "Ширина прямоугольника";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSquareSide;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Label lblGeoResult;
        private System.Windows.Forms.Label lblTextResult;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtInputText;
        private System.Windows.Forms.Label lblContactResult;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnPickFolder;
        private System.Windows.Forms.Button btnPickFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label lblFileResult;
        private System.Windows.Forms.Label lblSearchResult;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.TextBox txtSearchWord;
        private System.Windows.Forms.TextBox txtSearchPath;
        private System.Windows.Forms.TextBox txtRectW;
        private System.Windows.Forms.TextBox txtRectH;
        private System.Windows.Forms.TextBox txtTriangleBase;
        private System.Windows.Forms.TextBox txtTriangleH;
    }
}


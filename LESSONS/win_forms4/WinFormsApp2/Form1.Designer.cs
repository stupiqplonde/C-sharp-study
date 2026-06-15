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
            labelPlayer = new Label();
            labelDealer = new Label();
            labelResult = new Label();
            buttonAddCard = new Button();
            buttonCheckWinner = new Button();
            SuspendLayout();
            // 
            // labelPlayer
            // 
            labelPlayer.AutoSize = true;
            labelPlayer.Location = new Point(12, 15);
            labelPlayer.Name = "labelPlayer";
            labelPlayer.Size = new Size(46, 15);
            labelPlayer.TabIndex = 0;
            labelPlayer.Text = "Игрок:";
            // 
            // labelDealer
            // 
            labelDealer.AutoSize = true;
            labelDealer.Location = new Point(12, 45);
            labelDealer.Name = "labelDealer";
            labelDealer.Size = new Size(78, 15);
            labelDealer.TabIndex = 1;
            labelDealer.Text = "Компьютер:";
            // 
            // labelResult
            // 
            labelResult.Location = new Point(12, 75);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(360, 40);
            labelResult.TabIndex = 2;
            // 
            // buttonAddCard
            // 
            buttonAddCard.Location = new Point(12, 125);
            buttonAddCard.Name = "buttonAddCard";
            buttonAddCard.Size = new Size(170, 35);
            buttonAddCard.TabIndex = 3;
            buttonAddCard.Text = "Добавить карту";
            buttonAddCard.UseVisualStyleBackColor = true;
            buttonAddCard.Click += ButtonAddCard_Click;
            // 
            // buttonCheckWinner
            // 
            buttonCheckWinner.Location = new Point(202, 125);
            buttonCheckWinner.Name = "buttonCheckWinner";
            buttonCheckWinner.Size = new Size(170, 35);
            buttonCheckWinner.TabIndex = 4;
            buttonCheckWinner.Text = "Проверить кто выиграл";
            buttonCheckWinner.UseVisualStyleBackColor = true;
            buttonCheckWinner.Click += ButtonCheckWinner_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 181);
            Controls.Add(buttonCheckWinner);
            Controls.Add(buttonAddCard);
            Controls.Add(labelResult);
            Controls.Add(labelDealer);
            Controls.Add(labelPlayer);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Игра 21";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelPlayer;
        private Label labelDealer;
        private Label labelResult;
        private Button buttonAddCard;
        private Button buttonCheckWinner;
    }
}

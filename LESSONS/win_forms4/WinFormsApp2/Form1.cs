namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private readonly List<int> _playerCards = [];
        private readonly List<int> _dealerCards = [];
        private bool _gameFinished;

        public Form1()
        {
            InitializeComponent();
            UpdateLabels(hideDealerCards: true);
        }

        private void ButtonAddCard_Click(object? sender, EventArgs e)
        {
            if (_gameFinished)
            {
                return;
            }

            _playerCards.Add(DrawCard());
            UpdateLabels(hideDealerCards: true);

            if (GetSum(_playerCards) > 21)
            {
                _gameFinished = true;
                buttonAddCard.Enabled = false;
                labelResult.Text = "У вас перебор. Нажмите «Проверить», чтобы узнать результат.";
            }
        }

        private void ButtonCheckWinner_Click(object? sender, EventArgs e)
        {
            if (_playerCards.Count == 0)
            {
                MessageBox.Show(
                    "Сначала возьмите хотя бы одну карту.",
                    "Игра 21",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            while (GetSum(_dealerCards) < 17)
            {
                _dealerCards.Add(DrawCard());
            }

            _gameFinished = true;
            buttonAddCard.Enabled = false;
            UpdateLabels(hideDealerCards: false);

            int playerSum = GetSum(_playerCards);
            int dealerSum = GetSum(_dealerCards);
            string result = GetWinnerText(playerSum, dealerSum);
            labelResult.Text = result;

            MessageBox.Show(
                $"{result}{Environment.NewLine}{Environment.NewLine}" +
                $"Игрок: {playerSum}{Environment.NewLine}" +
                $"Компьютер: {dealerSum}",
                "Результат",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private static int DrawCard()
        {
            return Random.Shared.Next(2, 11);
        }

        private static int GetSum(IReadOnlyList<int> cards)
        {
            int sum = 0;
            foreach (int card in cards)
            {
                sum += card;
            }

            return sum;
        }

        private static string FormatCards(IReadOnlyList<int> cards)
        {
            return cards.Count == 0 ? "нет карт" : string.Join(", ", cards);
        }

        private void UpdateLabels(bool hideDealerCards)
        {
            int playerSum = GetSum(_playerCards);
            labelPlayer.Text = $"Игрок: {FormatCards(_playerCards)} | Сумма: {playerSum}";

            if (hideDealerCards && !_gameFinished)
            {
                labelDealer.Text = "Компьютер: карты скрыты";
            }
            else
            {
                int dealerSum = GetSum(_dealerCards);
                labelDealer.Text = $"Компьютер: {FormatCards(_dealerCards)} | Сумма: {dealerSum}";
            }
        }

        private static string GetWinnerText(int playerSum, int dealerSum)
        {
            bool playerBust = playerSum > 21;
            bool dealerBust = dealerSum > 21;

            if (playerBust && dealerBust)
            {
                return playerSum < dealerSum
                    ? "Победил игрок: у компьютера больше перебор."
                    : playerSum > dealerSum
                        ? "Победил компьютер: у игрока больше перебор."
                        : "Ничья: у обоих перебор.";
            }

            if (playerBust)
            {
                return "Победил компьютер: у игрока перебор.";
            }

            if (dealerBust)
            {
                return "Победил игрок: у компьютера перебор.";
            }

            if (playerSum > dealerSum)
            {
                return "Победил игрок!";
            }

            if (playerSum < dealerSum)
            {
                return "Победил компьютер!";
            }

            return "Ничья!";
        }
    }
}

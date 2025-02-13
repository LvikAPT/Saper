using System;
using System.Windows.Forms;

namespace Saper.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            StartGame(8, 8, 10); // Легкий уровень
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            StartGame(16, 16, 40); // Средний уровень
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            StartGame(24, 24, 99); // Сложный уровень
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Выход из приложения
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Правила игры: ...", "Правила"); // Показать правила игры
        }

        private void btnLeaderboard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Таблица лидеров: ...", "Таблица лидеров"); // Показать таблицу лидеров
        }

        private void StartGame(int rows, int cols, int mines)
        {
            GameForm gameForm = new GameForm(rows, cols, mines);
            gameForm.Show();
            this.Hide(); // Скрыть главную форму
        }
    }
}
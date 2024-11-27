using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent(); // Инициализация компонентов формы
        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            StartGame(0); // Легкий уровень
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            StartGame(1); // Средний уровень
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            StartGame(2); // Сложный уровень
        }

        private void StartGame(int difficulty)
        {
            GameForm gameForm = new GameForm(difficulty); // Создаем новую игру с заданной сложностью
            gameForm.Show(); // Показываем форму игры
            this.Hide(); // Скрываем главное меню
        }
    }
}
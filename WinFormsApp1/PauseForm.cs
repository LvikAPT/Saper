using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class PauseForm : Form
    {
        private GameForm gameForm;

        public PauseForm(GameForm gameForm)
        {
            InitializeComponent();
            this.gameForm = gameForm;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрываем окно паузы
            gameForm.ResumeGame(); // Возобновляем игру
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрываем окно паузы
            gameForm.GoToMainMenu(); // Переходим в главное меню
        }
    }
}
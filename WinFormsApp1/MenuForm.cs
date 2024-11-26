using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(0); // Легкий уровень
            gameForm.Show();
            this.Hide();
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(1); // Средний уровень
            gameForm.Show();
            this.Hide();
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(2); // Сложный уровень
            gameForm.Show();
            this.Hide();
        }
    }
}
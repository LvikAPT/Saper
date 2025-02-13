using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(10, 10, 10); // 10x10, 10 мин
            gameForm.Show();
            this.Hide();
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(16, 16, 40); // 16x16, 40 мин
            gameForm.Show();
            this.Hide();
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(30, 16, 99); // 30x16, 99 мин
            gameForm.Show();
            this.Hide();
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            string rules = System.IO.File.ReadAllText("rules.txt");
            MessageBox.Show(rules, "Правила игры");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
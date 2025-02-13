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

        private void btnResume_Click(object sender, EventArgs e)
        {
            gameForm.ResumeGame();
            this.Close();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Прогресс не сохранится. Вы уверены?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Close();
                gameForm.Close();
            }
        }
    }
}
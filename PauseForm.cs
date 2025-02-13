using System;
using System.Windows.Forms;

namespace Saper.Forms
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
            this.Close(); // Закрыть форму паузы и вернуться к игре
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            gameForm.Close(); // Закрываем игровое поле
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close(); // Закрыть форму паузы и открыть главное меню
        }
    }
}
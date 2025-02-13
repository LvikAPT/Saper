using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class ResultForm : Form
    {
        public ResultForm(bool isWin)
        {
            InitializeComponent();
            lblResult.Text = isWin ? "Ура! Вы победили!" : "Вы проиграли!";
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();
        }
    }
}
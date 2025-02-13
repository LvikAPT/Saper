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

        private void StartGame(int rows, int cols, int mines)
        {
            GameForm gameForm = new GameForm(rows, cols, mines);
            gameForm.Show();
        }
    }
}
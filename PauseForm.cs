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
            gameForm.ResumeGame();
            this.Close();
        }
    }
}
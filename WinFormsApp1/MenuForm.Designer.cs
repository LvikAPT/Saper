using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Устанавливаем фиксированный стиль окна
            this.MaximizeBox = false; // Запрещаем максимизацию окна
            this.StartPosition = FormStartPosition.CenterScreen; // Центрируем окно на экране

            string[] difficultyLevels = { "Легкий уровень", "Средний уровень", "Сложный уровень" };
            for (int i = 0; i < difficultyLevels.Length; i++)
            {
                Button btnDifficulty = new Button { Text = difficultyLevels[i], Location = new Point(10, 10 + i * 40) };
                btnDifficulty.Click += (s, e) => StartGame(i);
                this.Controls.Add(btnDifficulty);
            }

            Button btnExit = new Button { Text = "Выход", Location = new Point(10, 130) };
            btnExit.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnExit);

            this.Size = new Size(200, 200);
            this.BackColor = Color.LightGray; // Устанавливаем цвет фона
        }

        private void StartGame(int difficulty)
        {
            GameForm gameForm = new GameForm(difficulty);
            gameForm.Show();
            this.Hide();
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
    }
}
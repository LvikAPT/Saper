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
    }
}
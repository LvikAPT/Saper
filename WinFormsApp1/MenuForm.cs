using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent(); // Инициализация компонентов формы

            Button btnEasy = new Button { Text = "Легкий уровень", Location = new Point(10, 10) };
            btnEasy.Click += btnEasy_Click; // Легкий уровень
            this.Controls.Add(btnEasy);

            Button btnMedium = new Button { Text = "Средний уровень", Location = new Point(10, 50) };
            btnMedium.Click += btnMedium_Click; // Средний уровень
            this.Controls.Add(btnMedium);

            Button btnHard = new Button { Text = "Сложный уровень", Location = new Point(10, 90) };
            btnHard.Click += btnHard_Click; // Сложный уровень
            this.Controls.Add(btnHard);

            Button btnExit = new Button { Text = "Выход", Location = new Point(10, 130) };
            btnExit.Click += (s, e) => Application.Exit(); // Выход из приложения
            this.Controls.Add(btnExit);

            this.Size = new Size(200, 200);
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

        private void StartGame(int difficulty)
        {
            GameForm gameForm = new GameForm(difficulty); // Создаем новую игру с заданной сложностью
            gameForm.Show(); // Показываем форму игры
            this.Hide(); // Скрываем главное меню
        }
    }
}
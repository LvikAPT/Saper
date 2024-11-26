using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class GameForm : Form
    {
        private int rows;
        private int cols;
        private int mines;
        private Button[,] buttons;
        private Random random;
        private System.Windows.Forms.Timer timer;
        private int timeElapsed;
        private int minesLeft;
        private int cellsLeft; // Количество оставшихся клеток для открытия
        private bool isPaused = false; // Флаг для отслеживания состояния паузы

        public GameForm(int difficulty)
        {
            InitializeComponent();
            random = new Random();
            SetDifficulty(difficulty);
            CreateButtons();
            PlaceMines();
            StartTimer();
        }

        private void SetDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case 0: // Easy
                    rows = 8;
                    cols = 8;
                    mines = 10;
                    break;
                case 1: // Medium
                    rows = 16;
                    cols = 16;
                    mines = 40;
                    break;
                case 2: // Hard
                    rows = 16;
                    cols = 30;
                    mines = 99;
                    break;
            }
            minesLeft = mines;
            cellsLeft = rows * cols - mines; // Общее количество клеток минус мины
        }

        private void CreateButtons()
        {
            buttons = new Button[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(30, 30);
                    buttons[i, j].Location = new Point(j * 30, i * 30);
                    buttons[i, j].Tag = false; // Initially not a mine
                    buttons[i, j].Click += Button_Click;
                    buttons[i, j].MouseDown += Button_MouseDown; // Обработка нажатия мыши
                    this.Controls.Add(buttons[i, j]);
                }
            }
        }

        private void PlaceMines()
        {
            int placedMines = 0;
            while (placedMines < mines)
            {
                int row = random.Next(rows);
                int col = random.Next(cols);
                if (!(bool)buttons[row, col].Tag) // If not already a mine
                {
                    buttons[row, col].Tag = true; // Set as mine
                    placedMines++;
                }
            }
        }

        private void StartTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            this.Text = $"Сапёр - Время: {timeElapsed} секунд - Осталось мин: {minesLeft}";
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (isPaused) return; // Если игра на паузе, игнорируем клики

            Button clickedButton = sender as Button;

            if (clickedButton.Tag.Equals(true)) // If a mine is clicked
            {
                timer.Stop(); // Stop the timer
                RevealAllMines(); // Reveal all mines
                MessageBox.Show($"Вы проиграли! Осталось открыть мин: {minesLeft - 1}", "Поражение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close(); // Close the game form
            }
            else
            {
                OpenCell(clickedButton);
                CheckWinCondition();
            }
        }

        private void OpenCell(Button button)
        {
            if (button.Enabled)
            {
                button.Enabled = false; // Disable the button
                button.BackColor = Color.LightGray; // Change the button color to indicate it's open
                cellsLeft--; // Decrement the number of remaining cells to open

                // Удаляем звёздочку, если она есть
                if (button.Text == "*")
                {
                    button.Text = "";
                    minesLeft++; // Увеличиваем количество оставшихся мин
                }

                // Проверка количества соседних мин
                int adjacentMines = CountAdjacentMines(button);
                if (adjacentMines > 0)
                {
                    // Устанавливаем цвет текста в зависимости от количества соседних мин
                    button.ForeColor = GetColorForMineCount(adjacentMines);
                    button.Text = adjacentMines.ToString(); // Отображаем количество соседних мин
                }
                else
                {
                    // Если соседних мин нет, открываем соседние клетки
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int newRow = button.Location.Y / 30 + i;
                            int newCol = button.Location.X / 30 + j;
                            if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                            {
                                OpenCell(buttons[newRow, newCol]);
                            }
                        }
                    }
                }
            }
        }

        private int CountAdjacentMines(Button button)
        {
            int count = 0;
            int row = button.Location.Y / 30;
            int col = button.Location.X / 30;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newCol = col + j;
                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                    {
                        if (buttons[newRow, newCol].Tag.Equals(true))
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private Color GetColorForMineCount(int count)
        {
            switch (count)
            {
                case 1: return Color.Blue;
                case 2: return Color.Green;
                case 3: return Color.Red;
                case 4: return Color.DarkBlue;
                case 5: return Color.DarkRed;
                case 6: return Color.Cyan;
                case 7: return Color.Black;
                case 8: return Color.Gray;
                default: return Color.Black;
            }
        }

        private void RevealAllMines()
        {
            foreach (var button in buttons)
            {
                if (button.Tag.Equals(true))
                {
                    button.BackColor = Color.Red; // Показываем мины
                }
            }
        }

        private void CheckWinCondition()
        {
            if (cellsLeft == 0)
            {
                timer.Stop();
                MessageBox.Show("Поздравляем! Вы выиграли!", "Победа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Закрываем форму игры
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (isPaused)
                {
                    ResumeGame(); // Возобновляем игру
                }
                else
                {
                    PauseGame(); // Приостанавливаем игру
                }
                return true; // Обрабатываем событие
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PauseGame()
        {
            isPaused = true; // Устанавливаем флаг паузы
            timer.Stop(); // Останавливаем таймер
            PauseForm pauseForm = new PauseForm(this);
            pauseForm.ShowDialog(); // Показываем окно паузы
        }

        public void ResumeGame()
        {
            isPaused = false; // Сбрасываем флаг паузы
            timer.Start(); // Возобновляем таймер
        }

        public void GoToMainMenu()
        {
            // Логика для перехода в главное меню
            this.Close(); // Закрываем текущее окно игры
            MenuForm mainMenu = new MenuForm(); // Создаем новое окно главного меню
            mainMenu.Show(); // Показываем главное меню
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Логика для обработки правого клика, например, пометка мины
                Button clickedButton = sender as Button;
                if (clickedButton != null && clickedButton.Enabled)
                {
                    // Например, ставим звёздочку на кнопку
                    if (clickedButton.Text == "")
                    {
                        clickedButton.Text = "*"; // Помечаем мину
                        minesLeft--; // Уменьшаем количество оставшихся мин
                    }
                    else if (clickedButton.Text == "*")
                    {
                        clickedButton.Text = ""; // Убираем пометку
                        minesLeft++; // Увеличиваем количество оставшихся мин
                    }
                }
            }
        }
    }
}
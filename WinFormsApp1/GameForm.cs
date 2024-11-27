using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class GameForm : Form
    {
        private int rows; // Количество строк
        private int cols; // Количество столбцов
        private int mines; // Количество мин
        private Button[,] buttons; // Массив кнопок
        private Random random; // Генератор случайных чисел
        private System.Windows.Forms.Timer timer; // Таймер для отслеживания времени
        private int timeElapsed; // Время, прошедшее с начала игры
        private int minesLeft; // Количество оставшихся мин
        private int cellsLeft; // Количество оставшихся клеток для открытия
        private bool isPaused = false; // Флаг паузы

        public GameForm(int difficulty)
        {
            InitializeComponent();
            random = new Random();
            SetDifficulty(difficulty); // Установка сложности игры
            CreateButtons(); // Создание кнопок
            PlaceMines(); // Размещение мин
            StartTimer(); // Запуск таймера
            this.Size = new Size(cols * 30 + 20, rows * 30 + 100); // Установка размера формы
        }

        private void SetDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case 0: // Легкая сложность
                    rows = 8; cols = 8; mines = 10; break;
                case 1: // Средняя сложность
                    rows = 16; cols = 16; mines = 40; break;
                case 2: // Сложная сложность
                    rows = 16; cols = 30; mines = 99; break;
            }
            minesLeft = mines;
            cellsLeft = rows * cols - mines;
        }

        private void CreateButtons()
        {
            buttons = new Button[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Size = new Size(30, 30),
                        Location = new Point(j * 30, i * 30 + 50), // Учитываем место для таймера и кнопок
                        Tag = false // Изначально не мина
                    };
                    buttons[i, j].Click += Button_Click;
                    buttons[i, j].MouseDown += Button_MouseDown;
                    this.Controls.Add(buttons[i, j]);
                }
            }

            // Добавляем кнопки для перезапуска и выхода в меню
            Button btnRestart = new Button { Text = "Перезапустить", Location = new Point(10, 10) };
            btnRestart.Click += (s, e) => RestartGame();
            this.Controls.Add(btnRestart);

            Button btnMainMenu = new Button { Text = "В меню", Location = new Point(150, 10) };
            btnMainMenu.Click += (s, e) => GoToMainMenu();
            this.Controls.Add(btnMainMenu);
        }

        private void PlaceMines()
        {
            int placedMines = 0;
            while (placedMines < mines)
            {
                int row = random.Next(rows);
                int col = random.Next(cols);
                if (!(bool)buttons[row, col].Tag) // Если это не мина
                {
                    buttons[row, col].Tag = true; // Устанавливаем как мину
                    placedMines++;
                }
            }
        }

        private void StartTimer()
        {
            timer = new System.Windows.Forms.Timer { Interval = 1000 }; // 1 секунда
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            this.Text = $"Сапёр - Время: {timeElapsed} секунд - Осталось мин: {minesLeft}";

            // Проверка на истечение времени
            if (timeElapsed >= 300) // 5 минут
            {
                timer.Stop();
                MessageBox.Show("Время вышло! Вы проиграли.", "Поражение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RevealAllMines();
                GoToMainMenu();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (isPaused) return;
            Button button = sender as Button;

            if (button.Tag.Equals(true)) // Если нажата мина
            {
                timer.Stop(); // Останавливаем таймер
                RevealAllMines(); // Открываем все мины
                MessageBox.Show("Вы проиграли! Игра окончена.", "Поражение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GoToMainMenu(); // Возвращаемся в главное меню
            }
            else
            {
                OpenCell(button); // Открываем клетку
                CheckForWin(); // Проверяем, выиграл ли игрок
            }
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // Обработка правого клика для пометки мины
            {
                Button button = sender as Button;
                if (button.Enabled) // Если клетка еще не открыта
                {
                    button.BackColor = button.BackColor == Color.Red ? SystemColors.Control : Color.Red; // Меняем цвет для пометки
                }
            }
        }

        private void OpenCell(Button button)
        {
            if (button.Enabled && button.Tag.Equals(false)) // Если клетка не открыта и не является миной
            {
                button.Enabled = false; // Отключаем кнопку
                button.BackColor = Color.LightGray; // Меняем цвет на серый
                cellsLeft--; // Уменьшаем количество оставшихся клеток

                // Подсчитываем количество соседних мин
                int adjacentMines = CountAdjacentMines(button);
                if (adjacentMines > 0) // Если есть соседние мины
                {
                    button.ForeColor = GetColorForMineCount(adjacentMines); // Устанавливаем цвет текста
                    button.Text = adjacentMines.ToString(); // Отображаем количество соседних мин
                }
                else // Если соседних мин нет
                {
                    // Открываем все соседние клетки
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            // Пропускаем саму клетку
                            if (i == 0 && j == 0) continue;

                            int newRow = (button.Location.Y - 50) / 30 + i; // Корректируем Y-координату
                            int newCol = button.Location.X / 30 + j; // Корректируем X-координату

                            // Проверяем границы массива
                            if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                            {
                                OpenCell(buttons[newRow, newCol]); // Рекурсивно открываем соседние клетки
                            }
                        }
                    }
                }
            }
        }

        private int CountAdjacentMines(Button button)
        {
            int count = 0;
            int row = (button.Location.Y - 50) / 30; // Корректируем Y-координату
            int col = button.Location.X / 30; // Корректируем X-координату

            // Проверяем все соседние клетки
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // Пропускаем саму клетку
                    if (i == 0 && j == 0) continue;

                    int newRow = row + i;
                    int newCol = col + j;

                    // Проверяем границы массива
                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                    {
                        if (buttons[newRow, newCol].Tag.Equals(true)) // Если это мина
                        {
                            count++;
                        }
                    }
                }
            }
            return count; // Возвращаем количество соседних мин
        }

        private Color GetColorForMineCount(int count)
        {
            switch (count)
            {
                case 1: return Color.Blue; // 1 мина
                case 2: return Color.Green; // 2 мины
                case 3: return Color.Red; // 3 мины
                case 4: return Color.DarkBlue; // 4 мины
                case 5: return Color.Brown; // 5 мин
                case 6: return Color.Cyan; // 6 мин
                case 7: return Color.Black; // 7 мин
                case 8: return Color.Gray; // 8 мин
                default: return Color.Black; // На всякий случай
            }
        }

        private void CheckForWin()
        {
            if (cellsLeft == 0) // Если все клетки открыты
            {
                timer.Stop();
                MessageBox.Show("Поздравляем! Вы выиграли!", "Победа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GoToMainMenu();
            }
        }

        private void RevealAllMines()
        {
            foreach (Button button in buttons)
            {
                if (button.Tag.Equals(true)) // Если это мина
                {
                    button.BackColor = Color.Red; // Открываем мину
                }
            }
        }

        private void RestartGame()
        {
            // Логика перезапуска игры
            this.Controls.Clear(); // Очищаем текущие элементы управления
            CreateButtons(); // Создаем кнопки заново
            PlaceMines(); // Размещаем мины заново
            timeElapsed = 0; // Сбрасываем время
            StartTimer(); // Запускаем таймер заново
        }

            private void GoToMainMenu()
        {
            MenuForm menuForm = new MenuForm();
            menuForm.ShowDialog(); // Отображаем меню
            this.Close(); // Закрываем текущую форму
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Saper.Forms
{
    public partial class GameForm : Form
    {
        private int rows;
        private int cols;
        private int mines;
        private int flags;
        private System.Windows.Forms.Timer timer; // Поле таймера
        private int timeLeft;
        private bool[,] mineField; // Поле с минами
        private int[,] mineCount; // Количество мин вокруг каждой клетки
        private bool[,] revealed; // Открытые клетки
        private bool[,] flagged; // Помеченные клетки

        public GameForm(int rows, int cols, int mines)
        {
            InitializeComponent();
            this.rows = rows;
            this.cols = cols;
            this.mines = mines;
            this.flags = mines;
            InitializeGame();
        }

        private void InitializeGame()
        {
            timer = new System.Windows.Forms.Timer(); // Инициализация таймера
            timer.Interval = 1000; // 1 секунда
            timer.Tick += Timer_Tick;
            timeLeft = 300; // 5 минут
            timer.Start();

            // Инициализация игрового поля
            gameGrid.ColumnCount = cols;
            gameGrid.RowCount = rows;
            for (int i = 0; i < cols; i++)
            {
                gameGrid.Columns[i].Width = 30; // Ширина колонки
            }
            for (int i = 0; i < rows; i++)
            {
                gameGrid.Rows[i].Height = 30; // Высота строки
            }

            // Инициализация полей
            mineField = new bool[rows, cols];
            mineCount = new int[rows, cols];
            revealed = new bool[rows, cols];
            flagged = new bool[rows, cols];

            PlaceMines();
            CalculateMineCounts();
            UpdateFlagCount();
        }

        private void PlaceMines()
        {
            Random rand = new Random();
            for (int i = 0; i < mines; i++)
            {
                int row, col;
                do
                {
                    row = rand.Next(rows);
                    col = rand.Next(cols);
                } while (mineField[row, col]); // Повторяем, пока не найдем пустую ячейку
                mineField[row, col] = true; // Устанавливаем мину
            }
        }

        private void CalculateMineCounts()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (mineField[r, c])
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                int newRow = r + i;
                                int newCol = c + j;
                                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                                {
                                    mineCount[newRow, newCol]++;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            lblTime.Text = $"Осталось времени: {timeLeft} сек";
            if (timeLeft <= 0)
            {
                timer.Stop();
                MessageBox.Show("Время вышло! Вы проиграли!", "Проигрыш");
                this.Close();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer.Stop();
            PauseForm pauseForm = new PauseForm(this);
            pauseForm.Show();
        }

        public void ResumeGame()
        {
            timer.Start();
        }

        private void gameGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return; // Игнорируем заголовки

            // Логика открытия ячейки
            if (flagged[e.RowIndex, e.ColumnIndex]) return; // Если клетка помечена флажком, ничего не делаем

            if (mineField[e.RowIndex, e.ColumnIndex])
            {
                MessageBox.Show("Вы попали на мину! Игра окончена.", "Проигрыш");
                this.Close();
            }
            else
            {
                RevealCell(e.RowIndex, e.ColumnIndex);
                CheckWinCondition();
            }
        }

        private void RevealCell(int row, int col)
        {
            if (revealed[row, col]) return; // Если клетка уже открыта, ничего не делаем

            revealed[row, col] = true;
            gameGrid.Rows[row].Cells[col].Style.BackColor = Color.LightGreen;

            if (mineCount[row, col] > 0)
            {
                gameGrid.Rows[row].Cells[col].Value = mineCount[row, col]; // Показываем количество мин вокруг
            }
            else
            {
                // Если вокруг нет мин, открываем соседние клетки
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int newRow = row + i;
                        int newCol = col + j;
                        if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                        {
                            RevealCell(newRow, newCol);
                        }
                    }
                }
            }
        }

        private void gameGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Логика установки флажка
                if (revealed[e.RowIndex, e.ColumnIndex]) return; // Если клетка открыта, ничего не делаем

                flagged[e.RowIndex, e.ColumnIndex] = !flagged[e.RowIndex, e.ColumnIndex];
                gameGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = flagged[e.RowIndex, e.ColumnIndex] ? Color.Red : Color.White;
                UpdateFlagCount();
            }
        }

        private void UpdateFlagCount()
        {
            // Обновление отображения количества оставшихся флажков
            lblFlags.Text = $"Осталось флажков: {flags - CountFlags()}";
        }

        private int CountFlags()
        {
            int count = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (flagged[r, c]) count++;
                }
            }
            return count;
        }

        private void CheckWinCondition()
        {
            bool allCellsRevealed = true;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (!mineField[r, c] && !revealed[r, c])
                    {
                        allCellsRevealed = false;
                    }
                }
            }

            if (allCellsRevealed && CountFlags() == mines)
            {
                MessageBox.Show("Поздравляем! Вы выиграли!", "Победа");
                this.Close();
            }
        }
    }
}